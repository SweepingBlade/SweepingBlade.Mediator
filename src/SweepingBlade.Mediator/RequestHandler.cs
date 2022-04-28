namespace SweepingBlade.Mediator;

public abstract class RequestHandler<TRequest> : IRequestHandler<TRequest>
    where TRequest : IRequest
{
    protected abstract void Handle(TRequest request);

    public virtual Task HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        Handle(request);
        return Task.CompletedTask;
    }
}

public abstract class RequestHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    protected abstract TResponse Handle(TRequest request);

    public virtual Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Handle(request));
    }
}