using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpDotNetDemo.Data.Models
{
    public class OrderItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int? LotNumber { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufacturingDate { get; set; }
        public DateTime BestBeforeDate { get; set; }
    }
}
