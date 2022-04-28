namespace SweepingBlade.Mediator.Dispatchers;

public sealed class CommandDispatcher : ICommandDispatcher
{
    private readonly IDispatcher _dispatcher;

    public CommandDispatcher(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher ?? throw new ArgumentNullException(nameof(dispatcher));
    }

    public Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : notnull, ICommand
    {
        return _dispatcher.DispatchAsync(request, cancellationToken);
    }
}