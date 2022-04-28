namespace SweepingBlade.Mediator.Resolvers;

public interface IRequestHandlerResolver
{
    IRequestHandler<TRequest> Resolve<TRequest>() where TRequest : IRequest;
    IRequestHandler<TRequest, TResponse> Resolve<TRequest, TResponse>() where TRequest : IRequest<TResponse>;
}