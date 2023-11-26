using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    public class ProductImage : EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
