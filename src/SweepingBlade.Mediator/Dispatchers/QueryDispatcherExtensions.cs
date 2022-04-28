namespace SweepingBlade.Mediator.Dispatchers;

public static class QueryDispatcherExtensions
{
    public static TResponse Send<TRequest, TResponse>(this IQueryDispatcher dispatcher, TRequest request)
        where TRequest : IQuery<TResponse>
        where TResponse : notnull
    {
        return dispatcher.SendAsync<TRequest, TResponse>(request).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}