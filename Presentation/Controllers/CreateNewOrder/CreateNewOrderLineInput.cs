namespace Presentation.Controllers.CreateNewOrder
{
    /// <summary>
    /// order line input.
    /// </summary>
    public sealed class CreateNewOrderLineInput
    {
        /// <summary>
        /// Product identifier
        /// </summary>
        public int ProductId { get; set; }
        
        /// <summary>
        /// Product sku
        /// </summary>
        public string ProductSku { get; set; }
        
        /// <summary>
        /// Product quantity
        /// </summary>
        public int Quantity { get; set; }
        
        /// <summary>
        /// Total amount
        /// </summary>
        public decimal Amount { get; set; }
    }
}