using System.Net;
using System.Net.Sockets;

namespace maERP.SalesChannels;

public static class SalesChannelUrlValidator
{
    private static readonly string[] BlockedHosts =
    [
        "localhost",
        "metadata.google.internal",
        "169.254.169.254"
    ];

    /// <summary>
    /// Validates that a sales channel URL is safe for outbound HTTP requests.
    /// Blocks private/internal network addresses and non-HTTP(S) schemes to prevent SSRF attacks.
    /// </summary>
    public static void Validate(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("Sales channel URL must not be empty.");
        }

        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
        {
            throw new ArgumentException($"Sales channel URL is not a valid absolute URI: {url}");
        }

        if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
        {
            throw new ArgumentException($"Sales channel URL must use http or https scheme, got: {uri.Scheme}");
        }

        var host = uri.Host.ToLowerInvariant();

        foreach (var blocked in BlockedHosts)
        {
            if (host == blocked)
            {
                throw new ArgumentException($"Sales channel URL must not point to internal host: {host}");
            }
        }

        if (IPAddress.TryParse(host, out var ip))
        {
            if (IsPrivateOrReserved(ip))
            {
                throw new ArgumentException($"Sales channel URL must not point to a private or reserved IP address: {host}");
            }
        }
    }

    private static bool IsPrivateOrReserved(IPAddress ip)
    {
        if (IPAddress.IsLoopback(ip))
        {
            return true;
        }

        if (ip.AddressFamily == AddressFamily.InterNetworkV6 && ip.IsIPv6LinkLocal)
        {
            return true;
        }

        if (ip.AddressFamily != AddressFamily.InterNetwork)
        {
            return false;
        }

        byte[] bytes = ip.GetAddressBytes();

        return bytes[0] switch
        {
            10 => true,                                          // 10.0.0.0/8
            127 => true,                                         // 127.0.0.0/8
            172 => bytes[1] >= 16 && bytes[1] <= 31,             // 172.16.0.0/12
            192 => bytes[1] == 168,                              // 192.168.0.0/16
            169 => bytes[1] == 254,                              // 169.254.0.0/16 (link-local)
            0 => true,                                           // 0.0.0.0/8
            _ => false
        };
    }
}
