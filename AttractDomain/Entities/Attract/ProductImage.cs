﻿using Attract.Domain.Entities.Attract;
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
    [Table("ProductImage", Schema = "Attract")]

    public class ProductImage:EntityBase
    {
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public string ImageFileName1 { get; set; }
        public string ImageFileName2 { get; set; }
        public string ImageFileName3 { get; set; }
        public string ImageFileName4 { get; set; }
        public string ImageFileName5 { get; set; }
        public string ImageFileName6 { get; set; }
    }
}
