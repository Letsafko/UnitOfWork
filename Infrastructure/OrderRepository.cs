using System.Threading.Tasks;
using Domain;
using Domain.Abstracts.Database;

namespace Infrastructure
{
    public sealed class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }
        
        public Task AddOrderAsync(Order order)
        {
            return Insert(order);
        }

        public Task<Order> GetOrderById(int orderId)
        {
            return GetAsync(new Order { OrderId = orderId });
        }
    }
}