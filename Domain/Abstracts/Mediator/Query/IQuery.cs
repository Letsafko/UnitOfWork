using MediatR;
namespace Domain.Abstracts.Mediator.Query
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
    }
}