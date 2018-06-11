using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinisBack.Web.Models.Order
{
    public class OrderViewModel
    {
        public float Price { get; set; }
        public IList<OrderItemViewModel> OrderItems { get; set; }
    }

    public class OrderItemViewModel
    {
        public int SandwichId { get; set; }
        public float SandwichPrice { get; set; }
        public int Quantiy { get; set; }
        public float TotalPrice { get; set; }
    }
}