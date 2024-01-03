using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Cart;
using Attract.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Attract.API.Controllers.Cart
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ICartProductService _cartProductService;
        private readonly IAuthService _authService;

        public CartController(
            ICartService cartService, 
            ICartProductService cartProductService,
            IAuthService authService)
        {
            _cartService = cartService;
            _cartProductService = cartProductService;
            _authService = authService;
        }

        [HttpPost("AddToCart")]
        public async Task<ActionResult<BaseCommandResponse>> AddCartProduct(AddCartProductsDTO viewModel)
        {
            viewModel.CartId = await _cartService.GetCartByUser(_authService.GetCurrentUserId());
            if(viewModel.CartId == 0)
            {
                return Ok(new BaseCommandResponse
                {
                    Success = false,
                    Message = "Not Found.",
                });
            }
            return Ok(await _cartProductService.AddCartProduct(viewModel));
        }
        [HttpGet("GetCartProducts")]
        public async Task<ActionResult<BaseCommandResponse>> GetAll()
        {
            int CartId = await _cartService.GetCartByUser(_authService.GetCurrentUserId());
            if (CartId == 0)
            {
                return Ok(new BaseCommandResponse
                {
                    Success = false,
                    Message = "Not Found.",
                });
            }
            return Ok(await _cartProductService.GetAllCartProducts(CartId));
        }
        [HttpPut("UpdCtgry")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateCartProducts(UpdateCartProductsDTO viewModel)
        {
            return Ok(await _cartProductService.UpdateCartProducts(viewModel));
        }
    }
}
