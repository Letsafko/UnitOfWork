using System.Threading;
using System.Threading.Tasks;
using Domain.Abstracts.Database;
using Domain.Abstracts.Mediator.Command;
using MediatR;

namespace Application.CreateNewOrder
{
    public sealed class CreateNewOrderCommandHandler : ICommandHandler<CreateNewOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderLineRepository _orderLineRepository;
        private readonly ICreateNewOrderOutput _createNewOrderOutput;
        public CreateNewOrderCommandHandler(IOrderRepository orderRepository,
            IOrderLineRepository orderLineRepository,
            ICreateNewOrderOutput createNewOrderOutput)
        {
            _createNewOrderOutput = createNewOrderOutput;
            _orderLineRepository = orderLineRepository;
            _orderRepository = orderRepository;
        }

        public Task<Unit> Handle(CreateNewOrderCommand request, CancellationToken cancellationToken)
        {
            _createNewOrderOutput.Invalid("command is not well formatted.");
            // _orderRepository.AddOrderAsync()
            // _orderLineRepository.AddOrderLines()
            
            _createNewOrderOutput.Ok(new CreateOrderDto());
            return Task.FromResult(Unit.Value);
        }
    }
}