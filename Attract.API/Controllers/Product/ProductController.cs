using Attract.Common.BaseResponse;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.Product;
using Attract.Common.DTOs.Tag;
using Attract.Common.Helpers.ProductHelper;
using Attract.Service.IService;
using Attract.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using System.Text.Json;
using static Attract.Common.DTOs.Product.AddProductDTO;

namespace Attract.API.Controllers.Product
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IColorService _colorService;
        private readonly IAvailableSizeService _availableSizeService;
        private readonly ITagService _tagService;

        public ProductController(
            IProductService productService,
            IColorService colorService,
            IAvailableSizeService availableSizeService,
            ITagService tagService)
        {
            this.productService = productService;
            _colorService = colorService;
            _availableSizeService = availableSizeService;
            _tagService = tagService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult<BaseCommandResponse>> GetAllProducts([FromQuery]ProductPagination productPagination)
        {
            var products=await productService.GetAllProducts(productPagination);
            return Ok(products);
        }


        [HttpPost("AddProduct")]
        public async Task<ActionResult<BaseCommandResponse>> Add([FromForm] AddProductDTO viewModel)
        {
            if (viewModel.productQuantities.Where(x => x.Color.Id == 0).Any())
            {
                foreach (var item in viewModel.productQuantities.Where(x => x.Color.Id == 0).ToList())
                {
                    item.Color.Id =
                        await _colorService.AddColor(
                            new AddColorDTO
                            {
                                Name = item.Color.Name,
                                ColorHexa = item.Color.ColorHexa,
                            });
                }
            }
            if (viewModel.productQuantities.Where(x => x.Size.Id == 0).Any())
            {
                foreach (var item in viewModel.productQuantities.Where(x => x.Size.Id == 0).ToList())
                {
                    item.Size.Id =
                        await _availableSizeService.AddAvailableSize(
                            new AddAvailableSizeDTO
                            {
                                Name = item.Size.Name,
                            });
                }
            }
            if (viewModel.tags.Where(x => x.Id == 0).Any())
            {
                foreach (var item in viewModel.tags.Where(x => x.Id == 0).ToList())
                {
                    item.Id =
                        await _tagService.AddTag(
                            new AddTagDTO
                            {
                                Name = item.Name,
                            });
                }
            }
            var productId = await productService.AddProduct(viewModel);
            return Ok(productId);
        }

        [HttpPut("UpdateProduct")]
        public async Task<ActionResult<BaseCommandResponse>> UpdateProduct([FromForm] EditProductWithImageDTO editProductDTO)
        {
            var product = await productService.EditProductWithImageAsync(editProductDTO);
            return Ok(product);
        }
        [HttpDelete("DeletProduct/{id}")]
        public async Task<ActionResult<BaseCommandResponse>> DeleteProduct(int id)
        {
            var product = await productService.DeleteProduct(id);
            return Ok(product);
        }
    }
}
