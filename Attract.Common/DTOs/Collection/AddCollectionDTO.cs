using AttractDomain.Entities.Attract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Common.DTOs.Collection
{
    public class AddCollectionDTO
    {
        public int? ProductQuantityId { get; set; }
        public IFormFile Image1 { get; set; }
        public IFormFile Image2 { get; set; }
        public IFormFile Image3 { get; set; }
    }
    public class UpdateCollectionDTO : AddCollectionDTO
    {
        public int id { get; set; }
    }
}
