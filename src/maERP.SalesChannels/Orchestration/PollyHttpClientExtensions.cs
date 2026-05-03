using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace maERP.SalesChannels.Orchestration;

/// <summary>
/// Shared Polly policy chain applied to every channel-typed <c>HttpClient</c>: transient retry
/// with exponential jittered backoff, plus a circuit breaker that opens after 5 consecutive
/// failures for 30 seconds. 429-Retry-After is honored automatically by Polly's transient
/// detector.
/// </summary>
public static class PollyHttpClientExtensions
{
    public static IHttpClientBuilder AddPollyHandlers(this IHttpClientBuilder builder)
    {
        return builder
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(r => (int)r.StatusCode == 429)
                .WaitAndRetryAsync(3, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt))
                    + TimeSpan.FromMilliseconds(Random.Shared.Next(0, 250))))
            .AddPolicyHandler(HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30)));
    }
}
