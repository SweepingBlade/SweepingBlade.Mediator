namespace SweepingBlade.Mediator;

public abstract class QueryHandler<TQuery, TResponse> : RequestHandler<TQuery, TResponse>, IQueryHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}