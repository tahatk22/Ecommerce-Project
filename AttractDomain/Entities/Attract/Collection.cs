using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("Collection", Schema = "Attract")]
    public class Collection:EntityBase
    {
        public int Id { get; set; }
        public int? ProductQuantityId { get; set; }
        public virtual ProductQuantity ProductQuantity { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string Image3 { get; set; }
    }
}
