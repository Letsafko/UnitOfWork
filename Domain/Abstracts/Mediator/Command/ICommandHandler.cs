using MediatR;
namespace Domain.Abstracts.Mediator.Command
{
    public interface ICommandHandler<in TCommand> 
        : IRequestHandler<TCommand> where TCommand : ICommand
    {
    }
}