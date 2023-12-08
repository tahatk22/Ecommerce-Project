using Attract.Common.BaseResponse;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.SubCategory;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class CustomSubCategoryService : ICustomSubCategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CustomSubCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
        }

        public async Task<BaseCommandResponse> AddCustomSubCategory(CustomSubCategoryAddDto customSubCategoryDto)
        {
            var response = new BaseCommandResponse();
            var newSubCtgry = mapper.Map<CustomSubCategory>(customSubCategoryDto);
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/customsubcategory");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + customSubCategoryDto.ImgNm.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            newSubCtgry.ImgNm = uniqueFileName;
            newSubCtgry.CreatedBy = 1;
            await unitOfWork.GetRepository<CustomSubCategory>().InsertAsync(newSubCtgry);
            await unitOfWork.SaveChangesAsync();
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                customSubCategoryDto.ImgNm.CopyTo(fileStream);
                fileStream.Close();
            }
            response.Success = true;
            response.Data = newSubCtgry.Id;
            return response;
        }
    }
}
