﻿using Attract.Domain.Entities.Attract;
using Attract.Framework.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttractDomain.Entities.Attract
{
    [Table("OrderDetail", Schema = "Attract")]

    public class OrderDetail:EntityBase
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int? OrderId { get; set; }
        public virtual Order Order { get; set; }
    }
}
