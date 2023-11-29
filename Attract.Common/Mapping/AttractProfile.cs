using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Product;
using Attract.Domain.Entities.Attract;
using AttractDomain.Entities.Attract;
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


            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.AvailableSizes, opt => opt.MapFrom(src => src.ProductAvailableSizes.Select(pas => pas.AvailableSize.Name)))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(pc => pc.Color.Name)))
                // Map other properties as needed...
                ;
        }
    }
}
