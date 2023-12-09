using Attract.Common.DTOs;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.Product;
using Attract.Common.DTOs.SubCategory;
using Attract.Common.DTOs.User;
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

            CreateMap<AvailableSize,AvailableSizeDTO>().ReverseMap();
            CreateMap<Color,ColorDTO>().ReverseMap();
            CreateMap<User,UserDTO>().ReverseMap();
            CreateMap<User,LoginUserDTO>().ReverseMap();
            CreateMap<Product,EditProductDTO>().ReverseMap();
            CreateMap<Product,EditProductWithImageDTO>().ReverseMap();
            CreateMap<Product,AddProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.AvailableSizes, opt => opt.MapFrom(src => src.ProductAvailableSizes.Select(pas => pas.AvailableSize.Name)))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(pc => pc.Color.Name)))
                .ForMember(dest => dest.ImagePaths, opt => opt.MapFrom(src => src.Images.Select(pc => pc.Name)))
                .ForMember(dest => dest.ImgesHexa, opt => opt.MapFrom(src => src.Images.Select(pc => pc.ImageColorHexa)))
                .ForMember(dest => dest.ImgesColors, opt => opt.MapFrom(src => src.Images.Select(pc => pc.ImageColor))).ReverseMap();
                // Map other properties as needed...
                ;
            CreateMap<Category, CategoryDto>().ForMember(s=>s.SubCategories,tr=>tr.MapFrom(a=>a.SubCategories.Select(s=>s.SubCategoryName))).ReverseMap();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryUpdDto, Category>()
            .ForMember(dest => dest.ModifyOn, opt => opt.MapFrom(src => DateTime.UtcNow));
            /////////////////////////
            CreateMap<SubCategory, SubCategoryDto>().ForMember(s => s.Products, tr => tr.MapFrom(a => a.Products.Select(s => s.Name))).ReverseMap(); ;
            CreateMap<SubCategoryAddDto, SubCategory>();
            CreateMap<SubCategoryUpdDto, SubCategory>()
           .ForMember(dest => dest.ModifyOn, opt => opt.MapFrom(src => DateTime.UtcNow));

            //////////
          CreateMap<CustomSubCategoryAddDto, CustomSubCategory>()
            .ForMember(dest => dest.CreatedOn, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.ImgNm, opt => opt.Ignore());
            CreateMap<CustomSubCategory, CustomSubCategoryDto>();
            CreateMap<CustomSubCategoryUpdDto, CustomSubCategory>()
         .ForMember(dest => dest.ModifyOn, opt => opt.MapFrom(src => DateTime.UtcNow))
         .ForMember(dest => dest.ImgNm, opt => opt.Ignore());

        }
    }
}
