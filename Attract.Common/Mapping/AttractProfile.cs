using Attract.Common.DTOs;
using Attract.Domain.Entities.Attract;
using AutoMapper;

namespace Attract.Common.Mapping
{
    public class AttractProfile:Profile
    {
        public AttractProfile()
        {
            AttractMapper();
        }
        private void AttractMapper()
        {
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
