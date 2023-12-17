using Attract.Domain.Entities.Attract;
using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("Color", Schema = "Attract")]
    public class Color : EntityBase
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public string ColorHexa { get; set; }
        public ICollection<ProductQuantity> ProductQuantities { get; set; }

    }
}
