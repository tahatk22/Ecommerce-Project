using Attract.Common.BaseResponse;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface IImageService
    {
        Task<string> SaveImageAsync(IFormFile imageFile, string wwwrootPath);

    }
}
