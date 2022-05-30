using System.Collections.Generic;

namespace Presentation.Controllers.CreateNewOrder
{
    /// <summary>
    /// Create order input
    /// </summary>
    public class CreateNewOrderInput
    {
        /// <summary>
        /// order lines 
        /// </summary>
        public IEnumerable<CreateNewOrderLineInput> OrderLines { get; set; }
    }
}