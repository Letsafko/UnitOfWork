using System.Threading.Tasks;
using Domain;
using Domain.Abstracts.Database;
using Xunit;
namespace Integration.Test
{
    public class OrderRepositoryTest : IAsyncLifetime
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUnitOfWork _unitOfWork;
        public OrderRepositoryTest(IntegrationTestHost host)
        {
            _orderRepository = host.GetRequiredService<IOrderRepository>();
            _unitOfWork = host.GetRequiredService<IUnitOfWork>();
        }
        
        public Task InitializeAsync()
        {
            _unitOfWork.Begin();
            return _orderRepository.AddOrderAsync(new Order());
        }

        public Task DisposeAsync()
        {
            _unitOfWork.Rollback();
            return Task.CompletedTask;
        }
        
        
        [Fact]
        public async Task Order1()
        {
            var item = await _orderRepository.GetOrderById(1);
            Assert.NotNull(item);
        }
        
        [Fact]
        public void Order2()
        {
            Assert.True(true);
        }
    }
}