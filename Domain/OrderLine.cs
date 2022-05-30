namespace Domain
{
    public sealed class OrderLine
    {
        public string ProductSku { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}