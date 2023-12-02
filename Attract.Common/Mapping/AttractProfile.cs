﻿using Attract.Common.DTOs;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Product;
using Attract.Common.DTOs.SubCategory;
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

            CreateMap<Product,AddProductDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>()
                .ForMember(dest => dest.AvailableSizes, opt => opt.MapFrom(src => src.ProductAvailableSizes.Select(pas => pas.AvailableSize.Name)))
                .ForMember(dest => dest.Colors, opt => opt.MapFrom(src => src.ProductColors.Select(pc => pc.Color.Name)))
                .ForMember(dest => dest.ImagePaths, opt => opt.MapFrom(src => src.Images.Select(pc => pc.Name)))
                // Map other properties as needed...
                ;
            CreateMap<Category, CategoryDto>().ForMember(s=>s.SubCategories,tr=>tr.MapFrom(a=>a.SubCategories.Select(s=>s.SubCategoryName))).ReverseMap();
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryUpdDto, Category>()
            .ForMember(dest => dest.ModifyOn, opt => opt.MapFrom(src => DateTime.UtcNow));
            /////////////////////////
            CreateMap<SubCategory, SubCategoryDto>().ForMember(s => s.Products, tr => tr.MapFrom(a => a.Products.Select(s => s.Name))).ReverseMap(); ;

        }
    }
}
