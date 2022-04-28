using SweepingBlade.Mediator.Pipelines;

namespace SweepingBlade.Mediator.Resolvers;

public interface IPipelineBehaviorResolver
{
    IEnumerable<IPipelineBehavior<TRequest>> Resolve<TRequest>() where TRequest : IRequest;
    IEnumerable<IPipelineBehavior<TRequest, TResponse>> Resolve<TRequest, TResponse>() where TRequest : IRequest<TResponse>;
}