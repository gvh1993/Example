using Microsoft.Extensions.Logging;
using Padel.Application.Shared.Messaging;
using Padel.Domain.Shared;
using Serilog.Context;

namespace Padel.Application.Shared.Behaviours;

internal static class LoggingDecorator
{
    internal sealed class CommandHandler<TCommand, TResponse>(
        ICommandHandler<TCommand, TResponse> innerHandler,
        ILogger<CommandHandler<TCommand, TResponse>> logger)
        : ICommandHandler<TCommand, TResponse>
        where TCommand : ICommand<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;

            using (LogContext.PushProperty("Command", command, true))
            {
                logger.LogInformation("Processing command {CommandName}", commandName);
            }

            var result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                using (LogContext.PushProperty("Result", result, true))
                {
                    logger.LogInformation("Completed command {CommandName}", commandName);
                }
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Completed command {Command} with error", commandName);
                }
            }

            return result;
        }
    }

    internal sealed class CommandBaseHandler<TCommand>(
        ICommandHandler<TCommand> innerHandler,
        ILogger<CommandBaseHandler<TCommand>> logger)
        : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        public async Task<Result> Handle(TCommand command, CancellationToken cancellationToken)
        {
            var commandName = typeof(TCommand).Name;

            using (LogContext.PushProperty("Command", command, true))
            {
                logger.LogInformation("Processing command {CommandName}", commandName);
            }

            var result = await innerHandler.Handle(command, cancellationToken);

            if (result.IsSuccess)
            {
                using (LogContext.PushProperty("Result", result, true))
                {
                    logger.LogInformation("Completed command {CommandName}", commandName);
                }
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Completed command {CommandName} with error", commandName);
                }
            }

            return result;
        }
    }

    internal sealed class QueryHandler<TQuery, TResponse>(
        IQueryHandler<TQuery, TResponse> innerHandler,
        ILogger<QueryHandler<TQuery, TResponse>> logger)
        : IQueryHandler<TQuery, TResponse>
        where TQuery : IQuery<TResponse>
    {
        public async Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken)
        {
            var queryName = typeof(TQuery).Name;

            using (LogContext.PushProperty("Query", query, true))
            {
                logger.LogInformation("Processing query {QueryName}", queryName);
            }

            var result = await innerHandler.Handle(query, cancellationToken);

            if (result.IsSuccess)
            {
                using (LogContext.PushProperty("Result", result, true))
                {
                    logger.LogInformation("Completed query {QueryName}", queryName);
                }
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Completed query {QueryName} with error", queryName);
                }
            }

            return result;
        }
    }
}
