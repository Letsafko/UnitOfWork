using Domain.Abstracts.Mediator.Command;
using Domain.Abstracts.Mediator.Query;
namespace Domain.Abstracts.Mediator
{
    public interface IProcessor : IProcessCommand,
        IProcessQuery
    {
    }
}