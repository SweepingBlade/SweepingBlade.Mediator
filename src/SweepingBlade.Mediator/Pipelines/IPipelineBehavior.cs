namespace SweepingBlade.Mediator.Pipelines;

public interface IPipelineBehavior<in TRequest>
    where TRequest : IRequest
{
    Task HandleAsync(TRequest request, HandlerDelegate next, CancellationToken cancellationToken);
}

public interface IPipelineBehavior<in TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next, CancellationToken cancellationToken);
}