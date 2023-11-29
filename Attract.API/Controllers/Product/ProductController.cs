using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Product;
using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<BaseCommandResponse>> AddProduct(AddProductDTO productDTO)
        {
            var product = await productService.AddProduct(productDTO);
            return Ok(product);
        }
    }
}
