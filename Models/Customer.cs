using System;
using System.Collections.Generic;

namespace eCommerce.Models
{
    public class Customer : BaseEntity
    {
// -------------------------------------------------
        public int CustomerId { get; set; }
// -------------------------------------------------
        public string CustomerName { get; set; }
// -------------------------------------------------
        public DateTime CreatedAt { get; set; }
// -------------------------------------------------
        public DateTime UpdatedAt { get; set; }
// -------------------------------------------------
        public List<Order> Products { get; set; }
        public Customer()
        {
            Products = new List<Order>();
        }
// -------------------------------------------------

    }
}
