using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("Bill", Schema = "Attract")]

    public class Bill:EntityBase
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
