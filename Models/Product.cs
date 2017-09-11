using System;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public class Product : BaseEntity
    {
// -------------------------------------------------
        public int ProductId { get; set; }
// -------------------------------------------------
        public string ProductName { get; set; }
// -------------------------------------------------
        public string ImageUrl { get; set; }
// -------------------------------------------------
        public string Description { get; set; }
// -------------------------------------------------
        public int TotalQty { get; set; }
// -------------------------------------------------
        public DateTime CreatedAt { get; set; }
// -------------------------------------------------
        public DateTime UpdatedAt { get; set; }
// -------------------------------------------------
        public List<Order> Customers { get; set; }
        public Product()
        {
            Customers = new List<Order>();
        }
// -------------------------------------------------

    }
}
