using System.Threading.Tasks;

namespace Domain.Abstracts.Database
{
    public interface IOrderRepository
    {
        Task AddOrderAsync(Order order);
        Task<Order> GetOrderById(int orderId);
    }
}