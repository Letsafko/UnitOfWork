using System.Threading.Tasks;
using Domain.Abstracts.Mediator;
using Domain.Abstracts.Mediator.Command;
using Domain.Abstracts.Mediator.Query;
using MediatR;
namespace Application
{
    public sealed class Processor : IProcessor
    {
        private readonly IMediator _mediator;
        public Processor(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        public Task ProcessCommandAsync(ICommand command)
        {
            return _mediator.Send(command);
        }

        public Task<TResult> ProcessQueryAsync<TResult>(IQuery<TResult> query)
        {
            return _mediator.Send(query);
        }
    }
}