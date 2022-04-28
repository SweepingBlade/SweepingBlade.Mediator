namespace SweepingBlade.Mediator.Dispatchers;

public sealed class QueryDispatcher : IQueryDispatcher
{
    private readonly IDispatcher _dispatcher;

    public QueryDispatcher(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    public Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IQuery<TResponse>
        where TResponse : notnull
    {
        return _dispatcher.DispatchAsync<TRequest, TResponse>(request, cancellationToken);
    }
}