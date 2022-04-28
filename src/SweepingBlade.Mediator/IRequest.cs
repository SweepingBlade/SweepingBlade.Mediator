namespace SweepingBlade.Mediator;

public interface IRequest
{
}

public interface IRequest<TResponse> : IRequest
{
}