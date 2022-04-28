namespace SweepingBlade.Mediator.Pipelines;

public delegate Task HandlerDelegate();

public delegate Task<TResponse> HandlerDelegate<TResponse>();