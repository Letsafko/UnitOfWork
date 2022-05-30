using System.Threading.Tasks;
namespace Domain.Abstracts.Mediator.Command
{
    public interface IProcessCommand
    {
        Task ProcessCommandAsync(ICommand command);
    }
}