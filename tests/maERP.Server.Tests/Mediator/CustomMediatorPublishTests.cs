using maERP.Application.Mediator;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace maERP.Server.Tests.Mediator;

public class CustomMediatorPublishTests
{
    private sealed record TestNotification(string Payload) : INotification;

    private sealed class TrackingHandlerA : INotificationHandler<TestNotification>
    {
        public List<string> Calls { get; } = new();
        public Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            Calls.Add(notification.Payload);
            return Task.CompletedTask;
        }
    }

    private sealed class TrackingHandlerB : INotificationHandler<TestNotification>
    {
        public List<string> Calls { get; } = new();
        public Task Handle(TestNotification notification, CancellationToken cancellationToken)
        {
            Calls.Add(notification.Payload);
            return Task.CompletedTask;
        }
    }

    private sealed class ThrowingHandler : INotificationHandler<TestNotification>
    {
        public Task Handle(TestNotification notification, CancellationToken cancellationToken)
            => throw new InvalidOperationException("boom");
    }

    [Fact]
    public async Task Publish_WithMultipleHandlers_RunsAllOfThem()
    {
        var handlerA = new TrackingHandlerA();
        var handlerB = new TrackingHandlerB();

        var services = new ServiceCollection();
        services.AddSingleton<INotificationHandler<TestNotification>>(handlerA);
        services.AddSingleton<INotificationHandler<TestNotification>>(handlerB);
        services.AddSingleton<IMediator, CustomMediator>();
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();
        await mediator.Publish(new TestNotification("hello"));

        Assert.Single(handlerA.Calls);
        Assert.Equal("hello", handlerA.Calls[0]);
        Assert.Single(handlerB.Calls);
        Assert.Equal("hello", handlerB.Calls[0]);
    }

    [Fact]
    public async Task Publish_WithNoHandlers_IsNoOp()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IMediator, CustomMediator>();
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        await mediator.Publish(new TestNotification("noop"));
    }

    [Fact]
    public async Task Publish_WhenSingleHandlerThrows_RethrowsThatException()
    {
        var services = new ServiceCollection();
        services.AddSingleton<INotificationHandler<TestNotification>, ThrowingHandler>();
        services.AddSingleton<IMediator, CustomMediator>();
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(
            () => mediator.Publish(new TestNotification("kaboom")));
        Assert.Equal("boom", ex.Message);
    }

    [Fact]
    public async Task Publish_WhenOneHandlerThrows_OtherHandlersStillRun_AndAggregateIsThrown()
    {
        var goodHandler = new TrackingHandlerA();

        var services = new ServiceCollection();
        services.AddSingleton<INotificationHandler<TestNotification>>(goodHandler);
        services.AddSingleton<INotificationHandler<TestNotification>, ThrowingHandler>();
        services.AddSingleton<INotificationHandler<TestNotification>, ThrowingHandler>();
        services.AddSingleton<IMediator, CustomMediator>();
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        var aggregate = await Assert.ThrowsAsync<AggregateException>(
            () => mediator.Publish(new TestNotification("partial")));

        Assert.Equal(2, aggregate.InnerExceptions.Count);
        Assert.All(aggregate.InnerExceptions, e => Assert.IsType<InvalidOperationException>(e));
        Assert.Single(goodHandler.Calls);
        Assert.Equal("partial", goodHandler.Calls[0]);
    }

    [Fact]
    public async Task Publish_NullNotification_Throws()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IMediator, CustomMediator>();
        var provider = services.BuildServiceProvider();

        var mediator = provider.GetRequiredService<IMediator>();

        await Assert.ThrowsAsync<ArgumentNullException>(
            () => mediator.Publish<TestNotification>(null!));
    }
}
