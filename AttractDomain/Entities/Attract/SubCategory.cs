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
    [Table("SubCategory", Schema = "Attract")]

    public class SubCategory:EntityBase
    {
        public SubCategory()
        {
            Products=new HashSet<Product>();
        }
        public int Id { get; set; }
        [MaxLength(250)]
        public string SubCategoryName { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
