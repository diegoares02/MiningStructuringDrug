namespace MiningStructuringDrug.Core.Application.Common
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
