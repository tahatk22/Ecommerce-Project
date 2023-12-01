using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Image;
using Attract.Common.DTOs.Product;
using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using static Attract.Common.DTOs.Product.AddProductDTO;

namespace Attract.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet("GetAllSubCategoryProducts")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllSubCategoryProducts(int subCategoryId)
        {
            var products=await productService.GetAllSubCategoryProducts(subCategoryId);
            return Ok(products);
        }


        [HttpPost("AddProduct")]
        public async Task<ActionResult<BaseCommandResponse>> UploadImage([FromForm] AddProductWithImageDTO addProductImageDto)
        {
            // Validate and process the DTO as needed

            // Call the service to add the product image
            var productId = await productService.AddProductImageAsync(addProductImageDto);

            // Handle the result, e.g., return a response to the client
            return Ok(productId);
        }
    }
}
