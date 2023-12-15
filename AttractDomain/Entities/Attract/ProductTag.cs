using Attract.Domain.Entities.Attract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Domain.Entities.Attract
{
    public class ProductTag
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public int? TagId { get; set; }

        public virtual Product Product { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
