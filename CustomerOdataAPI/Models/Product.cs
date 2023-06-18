using System;
using System.Collections.Generic;

namespace CustomerOdataAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
