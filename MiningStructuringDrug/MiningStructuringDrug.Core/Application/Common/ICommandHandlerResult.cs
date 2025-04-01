namespace MiningStructuringDrug.Core.Application.Common
{
    public interface ICommandHandlerResult<in TCommand, TResult> where TCommand : ICommandResult<TResult>
    {
        Task<TResult> Handle(TCommand command);
    }
}
