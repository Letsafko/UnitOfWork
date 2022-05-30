using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Abstracts.Database
{
    public interface IOrderLineRepository
    {
        Task AddOrderLines(IEnumerable<OrderLine> orderLines);
    }
}