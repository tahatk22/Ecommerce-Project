using Microsoft.AspNetCore.Http;

namespace Attract.Common.DTOs.Image
{
    public class AddProductImageDTO
    {
        public List<string> ImageColorHexa { get; set; }
        public List<IFormFile> ImageFiles { get; set; }

    }
}
