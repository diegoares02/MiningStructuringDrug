using MiningStructuringDrug.Core.Application.Common;

namespace MiningStructuringDrug.Core.Application.Common
{
    public interface IMessageDispatcher
    {
        Task Dispatch<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> DispatchCommand<TCommand, TResult>(TCommand command) where TCommand : ICommandResult<TResult>;
        Task<TResult> DispatchQuery<TQuery, TResult>(TQuery query) where TQuery : IQuery<TResult>;
    }
}