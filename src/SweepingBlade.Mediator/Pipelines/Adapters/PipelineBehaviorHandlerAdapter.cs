using System.Reflection;

namespace SweepingBlade.Mediator.Pipelines.Adapters;

internal sealed class PipelineBehaviorHandlerAdapter<TRequest> : IHandlerAdapter where TRequest : notnull, IRequest
{
    private readonly CancellationToken _cancellationToken;
    private readonly object _instance;
    private readonly HandlerDelegate _next;
    private readonly TRequest _request;

    public PipelineBehaviorHandlerAdapter(TRequest request, object instance, HandlerDelegate next, CancellationToken cancellationToken)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        _cancellationToken = cancellationToken;
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public Task HandleAsync()
    {
        var pipelineBehaviorHandlerType = typeof(IPipelineBehavior<TRequest>);
        return (Task)pipelineBehaviorHandlerType.InvokeMember(
            nameof(IPipelineBehavior<TRequest>.HandleAsync),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
            null,
            _instance,
            new object[]
            {
                _request,
                _next,
                _cancellationToken
            });
    }
}

internal sealed class PipelineBehaviorHandlerAdapter<TRequest, TResponse> : IHandlerAdapter<TResponse> where TRequest : notnull, IRequest<TResponse>
{
    private readonly CancellationToken _cancellationToken;
    private readonly object _instance;
    private readonly HandlerDelegate<TResponse> _next;
    private readonly TRequest _request;

    public PipelineBehaviorHandlerAdapter(TRequest request, object instance, HandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        _cancellationToken = cancellationToken;
        _next = next ?? throw new ArgumentNullException(nameof(next));
    }

    public Task<TResponse> HandleAsync()
    {
        var pipelineBehaviorHandlerType = typeof(IPipelineBehavior<TRequest, TResponse>);
        return (Task<TResponse>)pipelineBehaviorHandlerType.InvokeMember(
            nameof(IPipelineBehavior<TRequest, TResponse>.HandleAsync),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
            null,
            _instance,
            new object[]
            {
                _request,
                _next,
                _cancellationToken
            });
    }
}