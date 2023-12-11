using Attract.Common.Validation;
using Microsoft.AspNetCore.Http;

namespace Attract.Common.DTOs.Image
{
    public class AddProductImageDTO
    {
        public List<string> ImageColorHexa { get; set; }

        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]

        public List<IFormFile> ImageFiles { get; set; }

    }
}
