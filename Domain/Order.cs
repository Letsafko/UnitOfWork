using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain
{
    [Table("order")]
    public sealed class Order
    {
        [Key]
        [Column("orderId")]
        public int OrderId { get; set; }
        public IEnumerable<OrderLine> OrderLines { get; set; }
    }
}