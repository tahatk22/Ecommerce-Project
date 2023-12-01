using Attract.Service.IService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class ImageService : IImageService
    {
        public ImageService()
        {
            
        }
        public async Task<string> SaveImageAsync(IFormFile imageFile, string wwwrootPath)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            // Generate a unique file name
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);

            // Combine wwwroot path and file name
            var filePath = Path.Combine(wwwrootPath, fileName);

            // Save the file to the wwwroot folder
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return fileName; // Return the file name to be stored in the database
        }
    }
}
