using Microsoft.AspNetCore.Http;

namespace Attract.Common.DTOs.Image
{
    public class ProductImageDTO
    {
        public List<IFormFile> ImageFiles { get; set; }

    }
}
