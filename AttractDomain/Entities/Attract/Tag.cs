using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Domain.Entities.Attract
{
    public class Tag : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProductTag> ProductTags { get; set; }
    }
}
