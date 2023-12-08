﻿using Attract.Common.BaseResponse;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.SubCategory;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        private readonly IHttpContextAccessor httpContextAccessor;
        public CustomSubCategoryService(IUnitOfWork unitOfWork, IMapper mapper, IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
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

        public async Task<BaseCommandResponse> GetAllCustomSubCategories()
        {
            var response = new BaseCommandResponse();
            var subCategories = await unitOfWork.GetRepository<CustomSubCategory>()
                .GetAllAsync();
            if (subCategories == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<IList<CustomSubCategoryDto>>(subCategories);
            //var httpContext = httpContextAccessor.HttpContext;
            //var imgdbNm =result.Where(x=>x.Id == ) ;
            // Get the host value
            //var hostValue = httpContext.Request.Host.Value;
            //var imgNm = $"{hostValue}/wwwroot/Images/customsubcategory/{imgdbNm}";
            response.Success = true;
            response.Data = result;
            return response;
        }
    }
}
