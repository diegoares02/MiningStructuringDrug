using Microsoft.Extensions.DependencyInjection;
using MiningStructuringDrug.Core.Application.Common;

namespace MiningStructuringDrug.Infrastructure.MessageDispatching
{
    public class MessageDispatcher : IMessageDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        public MessageDispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public async Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.Handle(command);
        }
        
        public async Task<TResult> DispatchCommand<TCommand, TResult>(TCommand command) where TCommand : ICommandResult<TResult>
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }
            var handler = _serviceProvider.GetRequiredService<ICommandHandlerResult<TCommand, TResult>>();

            return await handler.Handle(command);
        }
        
        public async Task<TResult> DispatchQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.Handle(query);
        }
    }
}
