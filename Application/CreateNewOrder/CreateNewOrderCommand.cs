using Domain.Abstracts.Mediator.Command;
namespace Application.CreateNewOrder
{
    public class CreateNewOrderCommand : ICommand
    {
        public CreateNewOrderCommand(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; }
    }
}