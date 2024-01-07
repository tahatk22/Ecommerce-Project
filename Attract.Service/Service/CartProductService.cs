using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Cart;
using Attract.Common.DTOs.SubCategory;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Attract.Service.Service
{
    public class CartProductService : ICartProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CartProductService(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> AddCartProduct(AddCartProductsDTO viewModel)
        {
            var cartProduct = await _unitOfWork.GetRepository<CartProduct>()
                .GetFirstOrDefaultAsync(predicate:
                x => x.CartId == viewModel.CartId &&
                x.ProductQuantityId == viewModel.ProductQuantityId);
            if (cartProduct != null)
            {
                cartProduct.Quantity += viewModel.Quantity;
                _unitOfWork.GetRepository<CartProduct>().UpdateAsync(cartProduct);
                await _unitOfWork.SaveChangesAsync();
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Quantity Updated",
                };
            }
            var cartProductToBeAdded = _mapper.Map<CartProduct>(viewModel);
            cartProductToBeAdded.CreatedBy = 1;
            await _unitOfWork.GetRepository<CartProduct>().InsertAsync(cartProductToBeAdded);
            await _unitOfWork.SaveChangesAsync();
            return new BaseCommandResponse
            {
                Success = true,
                Data = cartProductToBeAdded.Id
            };
        }
        public async Task<BaseCommandResponse> GetAllCartProducts(int cartId)
        {

            var response = new BaseCommandResponse();
            var cartProducts = await _unitOfWork.GetRepository<CartProduct>()
                .GetAllAsync(
                predicate: x => x.CartId == cartId,
                include: s =>
                s.Include(p => p.ProductQuantity)
                 .ThenInclude(p => p.Product)
                 .Include(p => p.ProductQuantity)
                .ThenInclude(x => x.Color)
                .Include(x => x.ProductQuantity)
                .ThenInclude(x => x.AvailableSize));
            if (cartProducts == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var items = _mapper.Map<List<CartProductItemsForGet>>(cartProducts);
            var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;
            foreach (var cartProduct in items)
            {
                //Update each ImageDTO in the collection
                var imageUrl = $"https://{hostValue}/Images/Product/{cartProduct.Image}";
                cartProduct.Image = imageUrl;
            }
            //var imageUrl = $"https://{hostValue}/Images/Product/{items.}";
            var result = new CartProductsDTO
            {
                CartProducts = items
            };
            response.Success = true;
            response.Data = result;
            return response;
        }

        public async Task<BaseCommandResponse> UpdateCartProducts(UpdateCartProductsDTO viewModel)
        {
            var cartProducts = await _unitOfWork.GetRepository<CartProduct>()
                .GetAllAsync(
                predicate: x => x.CartId == viewModel.CartId);
            if (cartProducts == null)
            {
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Not Found"
                };
            }
            if (viewModel.CartProducts.Count == 0)
            {
                foreach (var product in cartProducts)
                {
                    _unitOfWork.GetRepository<CartProduct>().Delete(product.Id);
                }
                await _unitOfWork.SaveChangesAsync();
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Updated Successfully, Now Cart Is Empty"
                };
            }

            foreach (var record in cartProducts)
            {
                if (!viewModel.CartProducts.Select(x => x.Id).Contains(record.Id))
                {
                    _unitOfWork.GetRepository<CartProduct>().Delete(record.Id);
                }
                else
                {
                    record.Quantity = viewModel.CartProducts.Where(x => x.Id == record.Id).Select(x => x.Quantity).FirstOrDefault();
                    record.ModifyOn = DateTime.Now;
                    _unitOfWork.GetRepository<CartProduct>().UpdateAsync(record);
                }
            }
            await _unitOfWork.SaveChangesAsync();
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Updated Successfully"
            };
        }
    }
}
