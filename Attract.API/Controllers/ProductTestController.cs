using Attract.Common.BaseResponse;
using Attract.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Attract.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTestController : ControllerBase
    {
        private readonly IProductTest productTest;

        public ProductTestController(IProductTest productTest)
        {
            this.productTest = productTest;
        }

        [HttpGet("GetProducts")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllProducts()
        {
            var products = await productTest.GetProducts();
            return Ok(products);
        }
    }
}
