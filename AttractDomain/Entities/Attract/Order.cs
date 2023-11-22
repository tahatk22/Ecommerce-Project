using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    public class Order:EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public string OrderNumber { get; set; }
    }
}
