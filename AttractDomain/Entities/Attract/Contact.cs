using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("Contact", Schema = "Attract")]

    public class Contact:EntityBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
    }
}
