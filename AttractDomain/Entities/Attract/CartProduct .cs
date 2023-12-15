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
    [Table("CartProduct", Schema = "Attract")]
    public class CartProduct : EntityBase
    {
        public int Id { get; set; }        
        public int CartId { get; set; }
        public int? ProductQuantityId { get; set; }
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual ProductQuantity ProductQuantity { get; set; }
    }
}
