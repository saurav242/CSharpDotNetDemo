using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpDotNetDemo.Data.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderValue { get; set; }
        public bool Shipped { get; set; }
        public DateTime ShipDate { get; set; }
        //public bool Delivered { get; set; }
        //public DateTime? DeliveryDate { get; set; }
        public IEnumerable<OrderItem> OrderItems { get; set; }

    }
}