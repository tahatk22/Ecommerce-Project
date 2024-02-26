using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("SliderValue", Schema = "Attract")]
    public class SliderValue
    {
        public int Id { get; set; }
        public bool Value { get; set; }
    }
}
