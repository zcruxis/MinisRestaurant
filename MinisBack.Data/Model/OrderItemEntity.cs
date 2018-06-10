using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinisBack.Data.Model
{
    public class OrderItemEntity
    {
        public int Id { get; set; }
        public int SandwichId { get; set; }
        public int Quantiy { get; set; }
        public float SandwichPrice { get; set; }
        public float TotalPrice { get; set; }
        public int OrderId { get; set; }

        [ForeignKey("OrderId")]
        public OrderEntity Order { get; set; }
    }
}
