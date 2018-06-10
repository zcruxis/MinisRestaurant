using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinisBack.Data.Model
{
    public class SandwichEntity
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public float Price { get; set; }
        public int SandwichTypeId { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
