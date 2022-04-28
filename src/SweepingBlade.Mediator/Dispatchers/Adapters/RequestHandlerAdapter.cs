using System.Reflection;

namespace SweepingBlade.Mediator.Dispatchers.Adapters;

internal sealed class RequestHandlerAdapter<TRequest> : IHandlerAdapter where TRequest : notnull, IRequest
{
    private readonly CancellationToken _cancellationToken;
    private readonly object _instance;
    private readonly TRequest _request;

    public RequestHandlerAdapter(TRequest request, object instance, CancellationToken cancellationToken)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        _cancellationToken = cancellationToken;
    }

    public Task HandleAsync()
    {
        var requestHandlerType = typeof(IRequestHandler<TRequest>);
        return (Task)requestHandlerType.InvokeMember(
            nameof(IRequestHandler<TRequest>.HandleAsync),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
            null,
            _instance,
            new object[]
            {
                _request,
                _cancellationToken
            });
    }
}

internal sealed class RequestHandlerAdapter<TRequest, TResponse> : IHandlerAdapter<TResponse> where TRequest : notnull, IRequest<TResponse>
{
    private readonly CancellationToken _cancellationToken;
    private readonly object _instance;
    private readonly TRequest _request;

    public RequestHandlerAdapter(TRequest request, object instance, CancellationToken cancellationToken)
    {
        _request = request ?? throw new ArgumentNullException(nameof(request));
        _instance = instance ?? throw new ArgumentNullException(nameof(instance));
        _cancellationToken = cancellationToken;
    }

    public Task<TResponse> HandleAsync()
    {
        var requestHandlerType = typeof(IRequestHandler<TRequest, TResponse>);
        return (Task<TResponse>)requestHandlerType.InvokeMember(
            nameof(IRequestHandler<TRequest, TResponse>.HandleAsync),
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.InvokeMethod,
            null,
            _instance,
            new object[]
            {
                _request,
                _cancellationToken
            });
    }
}