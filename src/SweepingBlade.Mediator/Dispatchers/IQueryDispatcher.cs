namespace SweepingBlade.Mediator.Dispatchers;

public interface IQueryDispatcher
{
    Task<TResponse> SendAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : IQuery<TResponse>
        where TResponse : notnull;
}