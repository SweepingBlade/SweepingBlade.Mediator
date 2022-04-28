namespace SweepingBlade.Mediator.Dispatchers;

public interface IDispatcher
{
    Task<TResponse> DispatchAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse> where TResponse : notnull;
    Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : notnull, IRequest;
}