﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Domain.Entities.Attract
{
    public class Category
    {
        public Category()
        {
            Products=new HashSet<Product>(); 
        }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
