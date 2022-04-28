namespace SweepingBlade.Mediator.Dispatchers;

public interface ICommandDispatcher
{
    Task SendAsync<TRequest>(TRequest request, CancellationToken cancellationToken = default)
        where TRequest : notnull, ICommand;
}