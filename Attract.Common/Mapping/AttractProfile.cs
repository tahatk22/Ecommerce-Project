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
            CreateMap<Product, ProductDTO>().ForMember(s=>s.Colors,tr=>tr.MapFrom(a=>a.Colors.Select(s=>s.Name)))
                .ForMember(s => s.AvailableSize, tr => tr.MapFrom(a => a.AvailableSizes.Select(s => s.Name))).ReverseMap();
        }
    }
}
