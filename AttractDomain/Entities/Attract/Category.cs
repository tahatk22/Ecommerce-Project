using Attract.Framework.Entity;
using AttractDomain.Entities.Attract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Attract.Domain.Entities.Attract
{
    [Table("Category", Schema = "Attract")]

    public class Category:EntityBase
    {
        public Category()
        {
            SubCategories = new HashSet<SubCategory>(); 
        }
        public int Id { get; set; }
        [MaxLength(250)]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<SubCategory> SubCategories { get; set; }

    }
}
