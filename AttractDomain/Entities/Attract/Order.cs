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
    [Table("Order", Schema = "Attract")]

    public class Order:EntityBase
    {
        public Order()
        {
            OrderDetails=new HashSet<OrderDetail>();    
        }
        public int Id { get; set; }
        [MaxLength(250)]
        [Required]
        public string Name { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public int OrderNumber { get; set; }
        [Required]
        public int Amount { get; set; }
        public virtual Bill Bill { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }

    }
}
