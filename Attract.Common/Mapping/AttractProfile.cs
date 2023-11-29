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
            CreateMap<Product, ProductDTO>().ForMember(s=>s.Colors,tr=>tr.MapFrom(a=>a.Colors.Select(s=>s.Name))).ReverseMap();
            CreateMap<Category, CategoryDto>().ForMember(s=>s.SubCategories,tr=>tr.MapFrom(a=>a.SubCategories.Select(s=>s.SubCategoryName))).ReverseMap();
        }
    }
}
