namespace SweepingBlade.Mediator.Dispatchers;

public static class CommandDispatcherExtensions
{
    public static void Send<TRequest>(this ICommandDispatcher dispatcher, TRequest request)
        where TRequest : ICommand
    {
        dispatcher.SendAsync(request).ConfigureAwait(false).GetAwaiter().GetResult();
    }
}