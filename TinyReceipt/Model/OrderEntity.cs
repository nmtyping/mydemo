using System;
using System.Collections.Generic;
using System.Text;

namespace TinyReceipt.Model
{
    public class OrderEntity
    {
        public int Num { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal TotalTax { get; set; }
    }
}
