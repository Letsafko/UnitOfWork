using System.Threading.Tasks;
namespace Domain.Abstracts.Mediator.Query
{
    public interface IProcessQuery
    {
        Task<TResult> ProcessQueryAsync<TResult>(IQuery<TResult> command);
    }
}