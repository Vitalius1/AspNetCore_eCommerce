using System;
namespace eCommerce.Models
{
    public class Order : BaseEntity
    {
// --------------------------------------------
        public int OrderId { get; set; }
// -------------------------------------------------
        public int OrderedQty { get; set; }
// --------------------------------------------
        public int ProductId { get; set; }
        public Product Product { get; set; }
// --------------------------------------------
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
// -------------------------------------------------
        public DateTime CreatedAt { get; set; }
// -------------------------------------------------
        public DateTime UpdatedAt { get; set; }
// -------------------------------------------------
    }
}