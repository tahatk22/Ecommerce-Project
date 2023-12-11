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
        public int? ProductId { get; set; }
        public int? AvailableSizeId { get; set; }
        public int? ColorId { get; set; }
        public int Quantity { get; set; }

        public virtual Cart Cart { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductAvailableSize ProductAvailableSize { get; set; }
        public virtual ProductColor ProductColor { get; set; }
    }
}
