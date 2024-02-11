using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("Country", Schema = "Attract")]
    public class Country:EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CountryFlag { get; set; }
    }
}
