namespace SweepingBlade.Mediator;

internal interface IHandlerAdapter
{
    Task HandleAsync();
}

internal interface IHandlerAdapter<TResponse>
{
    Task<TResponse> HandleAsync();
}