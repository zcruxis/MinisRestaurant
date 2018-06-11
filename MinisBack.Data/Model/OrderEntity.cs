using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MinisBack.Data.Model
{
    public class OrderEntity
    {
        public OrderEntity()
        {
            OrderItems = new HashSet<OrderItemEntity>();
        }
        [Key]
        public int Id { get; set; }
        public float Price { get; set; }
        public bool Paid { get; set; }
        public bool Completed { get; set; }
        public DateTime DateMade { get; set; }
        public DateTime? DatePaid { get; set; }
        public ICollection<OrderItemEntity> OrderItems { get; set; }
    }
}
