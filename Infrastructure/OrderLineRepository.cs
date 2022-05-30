using System.Collections.Generic;
using System.Threading.Tasks;
using Domain;
using Domain.Abstracts.Database;

namespace Infrastructure
{
    public sealed class OrderLineRepository : BaseRepository<OrderLine>, IOrderLineRepository
    {
        public OrderLineRepository(IUnitOfWork unitOfWork) 
            : base(unitOfWork)
        {
        }

        public Task AddOrderLines(IEnumerable<OrderLine> orderLines)
        {
            throw new System.NotImplementedException();
        }
    }
}