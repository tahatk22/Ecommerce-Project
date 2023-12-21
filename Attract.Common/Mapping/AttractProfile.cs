﻿using Attract.Common.DTOs;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Cart;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.Image;
using Attract.Common.DTOs.Product;
using Attract.Common.DTOs.SubCategory;
using Attract.Common.DTOs.Tag;
using Attract.Common.DTOs.User;
using Attract.Domain.Entities.Attract;
using AttractDomain.Entities.Attract;
using AutoMapper;

namespace Attract.Common.Mapping
{
    public class AttractProfile : Profile
    {
        public AttractProfile()
        {
            AttractMapper();
        }
        private void AttractMapper()
        {

            CreateMap<ProductImage, ImageDTO>()
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImageFileName))
                .ForMember(dest => dest.ImgesHexa, opt => opt.MapFrom(src => src.ImageColorHexa))
                .ForMember(dest => dest.ImgesColor, opt => opt.MapFrom(src => src.ImageColor))
                .ReverseMap();

            CreateMap<AvailableSize, AvailableSizeDTO>().ReverseMap();
            CreateMap<Color, ColorDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<EditProductDTO, Product>();
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.ProductQuantities, opt => opt.Ignore())
                .ForMember(dest => dest.ProductTags, opt => opt.Ignore());

            CreateMap<Product, ProductDTO>().ReverseMap();
            //.ForMember(dest => dest.AvailableSizes, opt => opt.MapFrom(src => src.ProductAvailableSizes.Select(pas => pas.AvailableSize.Name)))
            //.ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images)).ReverseMap();

            CreateMap<Category, CategoryDto>().ForMember(s => s.SubCategories, tr => tr.MapFrom(a => a.SubCategories.Select(s => s.SubCategoryName))).ReverseMap();
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


            #region Cart

            CreateMap<CartProduct, CartProductItemsForGet>()
                 .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.ProductQuantity.Product.Name))
                 .ForMember(dst => dst.ProductPrice, opt => opt.MapFrom(src => src.ProductQuantity.Price))
                 .ForMember(dst => dst.ColorName, opt => opt.MapFrom(src => src.ProductQuantity.Color.Name))
                 .ForMember(dst => dst.AvailableSizeName, opt => opt.MapFrom(src => src.ProductQuantity.AvailableSize.Name)).ReverseMap();
            CreateMap<AddCartProductsDTO, CartProduct>();
            CreateMap<CartProductItemForUpdate, AddCartProductsDTO>();

            #endregion

            #region Color

            CreateMap<Color, ColorDTO>().ReverseMap();
            CreateMap<Color, AddColorDTO>().ReverseMap();
            CreateMap<Color, UpdateColorDTO>().ReverseMap();

            #endregion

            #region AvailableSize

            CreateMap<AvailableSize, AvailableSizeDTO>().ReverseMap();
            CreateMap<AvailableSize, AddAvailableSizeDTO>().ReverseMap();
            CreateMap<AvailableSize, UpdateAvailableSizeDTO>().ReverseMap();

            #endregion

            #region Tag

            CreateMap<Tag, TagDTO>().ReverseMap();
            CreateMap<Tag, AddTagDTO>().ReverseMap();
            CreateMap<Tag, UpdateTagDTO>().ReverseMap();

            #endregion

        }
    }
}
