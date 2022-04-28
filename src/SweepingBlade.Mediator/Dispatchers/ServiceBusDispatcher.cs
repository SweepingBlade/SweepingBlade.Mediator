using SweepingBlade.Mediator.Dispatchers.Adapters;
using SweepingBlade.Mediator.Pipelines;
using SweepingBlade.Mediator.Pipelines.Adapters;
using SweepingBlade.Mediator.Resolvers;

namespace SweepingBlade.Mediator.Dispatchers;

public class ServiceBusDispatcher : IDispatcher
{
    private readonly IPipelineBehaviorResolver _pipelineBehaviorResolver;
    private readonly IRequestHandlerResolver _requestHandlerResolver;

    public ServiceBusDispatcher(IRequestHandlerResolver requestHandlerResolver, IPipelineBehaviorResolver pipelineBehaviorResolver)
    {
        _requestHandlerResolver = requestHandlerResolver ?? throw new ArgumentNullException(nameof(requestHandlerResolver));
        _pipelineBehaviorResolver = pipelineBehaviorResolver ?? throw new ArgumentNullException(nameof(pipelineBehaviorResolver));
    }

    public virtual Task<TResponse> DispatchAsync<TRequest, TResponse>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse> where TResponse : notnull
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        // ReSharper disable once JoinDeclarationAndInitializer
        HandlerDelegate<TResponse> currentHandlerDelegate;
        var requestHandlerInstance = _requestHandlerResolver.Resolve<TRequest, TResponse>();

        var requestHandlerAdapter = CreateRequestHandlerAdapter(request, requestHandlerInstance, cancellationToken);
        currentHandlerDelegate = requestHandlerAdapter.HandleAsync;

        var pipelineHandlerInstances = _pipelineBehaviorResolver.Resolve<TRequest, TResponse>();

        foreach (var pipelineHandlerInstance in pipelineHandlerInstances)
        {
            var pipelineBehaviorHandlerAdapter = CreatePipelineBehaviorHandlerAdapter(request, pipelineHandlerInstance, currentHandlerDelegate, cancellationToken);
            currentHandlerDelegate = pipelineBehaviorHandlerAdapter.HandleAsync;
        }

        return currentHandlerDelegate();
    }

    public virtual Task DispatchAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default) where TRequest : IRequest
    {
        if (request is null) throw new ArgumentNullException(nameof(request));

        // ReSharper disable once JoinDeclarationAndInitializer
        HandlerDelegate currentHandlerDelegate;
        var requestHandlerInstance = _requestHandlerResolver.Resolve<TRequest>();

        var requestHandlerAdapter = CreateRequestHandlerAdapter(request, requestHandlerInstance, cancellationToken);
        currentHandlerDelegate = requestHandlerAdapter.HandleAsync;

        var pipelineHandlerInstances = _pipelineBehaviorResolver.Resolve<TRequest>();

        foreach (var pipelineHandlerInstance in pipelineHandlerInstances)
        {
            var pipelineBehaviorHandlerAdapter = CreatePipelineBehaviorHandlerAdapter(request, pipelineHandlerInstance, currentHandlerDelegate, cancellationToken);
            currentHandlerDelegate = pipelineBehaviorHandlerAdapter.HandleAsync;
        }

        return currentHandlerDelegate();
    }

    private static PipelineBehaviorHandlerAdapter<TRequest, TResponse> CreatePipelineBehaviorHandlerAdapter<TRequest, TResponse>(TRequest request, IPipelineBehavior<TRequest, TResponse> pipelineBehavior, HandlerDelegate<TResponse> handlerDelegate, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
        where TResponse : notnull
    {
        return new PipelineBehaviorHandlerAdapter<TRequest, TResponse>(request, pipelineBehavior, handlerDelegate, cancellationToken);
    }

    private static PipelineBehaviorHandlerAdapter<TRequest> CreatePipelineBehaviorHandlerAdapter<TRequest>(TRequest request, IPipelineBehavior<TRequest> pipelineBehavior, HandlerDelegate handlerDelegate, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        return new PipelineBehaviorHandlerAdapter<TRequest>(request, pipelineBehavior, handlerDelegate, cancellationToken);
    }

    private static RequestHandlerAdapter<TRequest, TResponse> CreateRequestHandlerAdapter<TRequest, TResponse>(TRequest request, IRequestHandler<TRequest, TResponse> requestHandler, CancellationToken cancellationToken)
        where TRequest : IRequest<TResponse>
        where TResponse : notnull
    {
        return new RequestHandlerAdapter<TRequest, TResponse>(request, requestHandler, cancellationToken);
    }

    private static RequestHandlerAdapter<TRequest> CreateRequestHandlerAdapter<TRequest>(TRequest request, IRequestHandler<TRequest> requestHandler, CancellationToken cancellationToken)
        where TRequest : IRequest
    {
        return new RequestHandlerAdapter<TRequest>(request, requestHandler, cancellationToken);
    }
}