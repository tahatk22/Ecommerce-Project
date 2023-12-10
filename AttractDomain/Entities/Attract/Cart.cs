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
    [Table("Cart", Schema = "Attract")]
    public class Cart : EntityBase
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }

        public User User { get; set; }
        public ICollection<CartProduct> CartProducts { get; set; }
    }
}
