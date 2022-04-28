namespace SweepingBlade.Mediator.Pipelines;

public abstract class PipelineBehavior<TRequest> : IPipelineBehavior<TRequest>
    where TRequest : IRequest
{
    public abstract Task HandleAsync(TRequest request, HandlerDelegate next, CancellationToken cancellationToken);
}

public abstract class PipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> HandleAsync(TRequest request, HandlerDelegate<TResponse> next, CancellationToken cancellationToken);
}