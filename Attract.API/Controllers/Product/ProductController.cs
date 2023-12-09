using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Image;
using Attract.Common.DTOs.Product;
using Attract.Common.Helpers.ProductHelper;
using Attract.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using static Attract.Common.DTOs.Product.AddProductDTO;

namespace Attract.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllProducts([FromQuery]ProductPagination productPagination)
        {
            var products=await productService.GetAllProducts(productPagination);
            return Ok(products);
        }


        [HttpPost("AddProduct")]
        public async Task<ActionResult<BaseCommandResponse>> UploadImage([FromForm] AddProductWithImageDTO addProductImageDto)
        {
            var productId = await productService.AddProductImageAsync(addProductImageDto);
            return Ok(productId);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateProduct([FromForm] EditProductWithImageDTO editProductDTO)
        {
            var product = await productService.EditProductWithImageAsync(editProductDTO);
            return Ok(product);
        }
    }
}
