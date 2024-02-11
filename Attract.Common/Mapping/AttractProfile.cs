using Attract.Common.DTOs;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Cart;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Contact;
using Attract.Common.DTOs.Country;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.Product;
using Attract.Common.DTOs.Slider;
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
            CreateMap<Contact, AddContactDTO>().ReverseMap();
            CreateMap<Contact,ContactDTO>().ReverseMap();


            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, LoginUserDTO>().ReverseMap();
            CreateMap<EditProductDTO, Product>();
            CreateMap<AddProductDTO, Product>()
                .ForMember(dest => dest.ProductQuantities, opt => opt.Ignore())
                .ForMember(dest => dest.ProductTags, opt => opt.Ignore());

            CreateMap<EditProductQty, ProductQuantity>();

            CreateMap<Product, ProductDTO>()
              .ForMember(dest => dest.ProductQuantities, opt => opt.MapFrom(src => src.ProductQuantities))
              .ForMember(dest => dest.DiscountOptionName, opt => opt.MapFrom(src => Enum.GetName(src.DiscountOption)))
              .ForMember(dest => dest.ProductTypeOptionName, opt => opt.MapFrom(src => Enum.GetName(src.ProductTypeOption)));

             CreateMap<ProductQuantity, ProductQuantityDTO>();

            CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.SubCategories, opt => opt.MapFrom(src => src.SubCategories));
            CreateMap<CategoryAddDto, Category>();
            CreateMap<CategoryUpdDto, Category>()
            .ForMember(dest => dest.ModifyOn, opt => opt.MapFrom(src => DateTime.UtcNow));
            /////////////////////////
            CreateMap<SubCategory, SubCategoryDto>().ReverseMap(); ;
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

            #region Country
            CreateMap<Country, AddCountryDTO>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            #endregion

            #region Cart

            CreateMap<CartProduct, CartProductItemsForGet>()
                 .ForMember(dst => dst.ProductName, opt => opt.MapFrom(src => src.ProductQuantity.Product.Name))
                 .ForMember(dst => dst.Image, opt => opt.MapFrom(src => src.ProductQuantity.ImageName))
                 .ForMember(dst => dst.ProductPrice, opt => opt.MapFrom(src => src.ProductQuantity.Price))
                 .ForMember(dst => dst.ColorName, opt => opt.MapFrom(src => src.ProductQuantity.Color.Name))
                 .ForMember(dst => dst.ColorId, opt => opt.MapFrom(src => src.ProductQuantity.ColorId))
                 .ForMember(dst => dst.AvailableSizeName, opt => opt.MapFrom(src => src.ProductQuantity.AvailableSize.Name))
                 .ForMember(dst => dst.AvailableSizeId, opt => opt.MapFrom(src => src.ProductQuantity.AvailableSizeId)).ReverseMap();
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
            #region Slider
            CreateMap<Slider, SliderDto>().ReverseMap();
            #endregion

        }
    }
}
