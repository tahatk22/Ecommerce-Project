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
    [Table("CustomSubCategory", Schema = "Attract")]
    public class CustomSubCategory : EntityBase
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string CategoryName { get; set; }
        [MaxLength(250)]
        public string SubCategoryName { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(250)]
        public string ImgNm { get; set; }
    }
}
