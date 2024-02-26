using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Cart;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ICartProductService
    {
        Task<BaseCommandResponse> AddCartProduct(AddCartProductsDTO viewModel);
        Task<BaseCommandResponse> GetAllCartProducts(int cartId);
        Task<BaseCommandResponse> UpdateCartProducts(UpdateCartProductsDTO viewModel);
        Task<BaseCommandResponse> DeleteCartProduct(int id);
    }
}
