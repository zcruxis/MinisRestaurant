using MinisBack.Data.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinisBack.Data.Model
{
    public class SandwichTypeEntity
    {
        public int Id { get; set; }
        [StringLength(255)]
        public string Name { get; set; }
        public SandwichCutType Cut { get; set; }
        public SandwichSalsaType Salsa { get; set; }
    }
}
