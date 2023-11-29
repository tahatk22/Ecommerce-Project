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
    [Table("AvailableSize", Schema = "Attract")]
    public class AvailableSize : EntityBase
    {
        public AvailableSize()
        {
            ProductAvailableSizes=new HashSet<ProductAvailableSize>();
        }
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public ICollection<ProductAvailableSize> ProductAvailableSizes { get; set; }

    }
}
