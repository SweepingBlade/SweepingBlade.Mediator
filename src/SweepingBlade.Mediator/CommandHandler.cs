namespace SweepingBlade.Mediator;

public abstract class CommandHandler<TCommand> : RequestHandler<TCommand>, ICommandHandler<TCommand>
    where TCommand : ICommand
{
}