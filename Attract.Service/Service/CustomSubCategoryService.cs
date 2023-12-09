using Attract.Common.BaseResponse;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.SubCategory;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Hosting.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public CustomSubCategoryService(IUnitOfWork unitOfWork, IMapper mapper, 
            IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> AddCustomSubCategory(CustomSubCategoryAddDto customSubCategoryDto)
        {
            var response = new BaseCommandResponse();
            //var userId = httpContextAccessor.HttpContext.User?.FindFirstValue("UserID");
            var newSubCtgry = mapper.Map<CustomSubCategory>(customSubCategoryDto);
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/customsubcategory");
            string uniqueFileName = Guid.NewGuid().ToString() + "_" + customSubCategoryDto.imgNm.FileName;
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);
            newSubCtgry.ImgNm = uniqueFileName;
            newSubCtgry.CreatedBy = 1;//Convert.ToInt32(userId);
            await unitOfWork.GetRepository<CustomSubCategory>().InsertAsync(newSubCtgry);
            await unitOfWork.SaveChangesAsync();
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                customSubCategoryDto.imgNm.CopyTo(fileStream);
                fileStream.Close();
            }
            response.Success = true;
            response.Data = newSubCtgry.Id;
            return response;
        }

        public async Task<BaseCommandResponse> DeleteCustomSubCategory(int CustomSubCategoryId)
        {
            var response = new BaseCommandResponse();
            var subCategory = await unitOfWork.GetRepository<CustomSubCategory>().GetFirstOrDefaultAsync(predicate: x => x.Id == CustomSubCategoryId);
            if (subCategory == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<CustomSubCategoryDto>(subCategory);
            unitOfWork.GetRepository<CustomSubCategory>().Delete(subCategory.Id);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
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
            foreach (var item in result)
            {
                //Get the host value
                var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;
                item.imgUrl = $"https://{hostValue}/Images/customsubcategory/{item.imgNm}";
            }
            response.Success = true;
            response.Data = result;
            return response;
        }

        public async Task<BaseCommandResponse> GetCustomSubCategory(int CustomSubCategoryId)
        {
            var response = new BaseCommandResponse();
            var subCategory = await unitOfWork.GetRepository<CustomSubCategory>().GetFirstOrDefaultAsync(predicate: x => x.Id == CustomSubCategoryId);
            if (subCategory == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<CustomSubCategoryDto>(subCategory);
            //Get the host value
            var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;
            result.imgUrl = $"https://{hostValue}/Images/customsubcategory/{result.imgNm}";
            response.Success = true;
            response.Data = result;
            return response;
        }

        public async Task<BaseCommandResponse> UpdCustomSubCategory(CustomSubCategoryUpdDto customSubCategoryUpdDto)
        {
            bool isImageUpdated = false;
            string uniqueFileName = string.Empty, filePath = string.Empty;
            var response = new BaseCommandResponse();
            // Retrieve the existing category from the database
            var existingSubCategory = await unitOfWork.GetRepository<CustomSubCategory>().GetFirstOrDefaultAsync(predicate: x => x.Id == customSubCategoryUpdDto.id);

            if (existingSubCategory == null)
            {
                response.Success = false;
                response.Message = "Custom SubCategory not found.";
                return response;
            }
            
            if (customSubCategoryUpdDto.imgNm != null)
            {
                isImageUpdated = true;
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/customsubcategory");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + customSubCategoryUpdDto.imgNm.FileName;
                filePath = Path.Combine(uploadsFolder, uniqueFileName);
            }
            var newSubCtgry = mapper.Map<CustomSubCategory>(customSubCategoryUpdDto);
            newSubCtgry.CreatedBy = existingSubCategory.CreatedBy;
            newSubCtgry.CreatedOn = existingSubCategory.CreatedOn;
            newSubCtgry.ImgNm = existingSubCategory.ImgNm;
            unitOfWork.GetRepository<CustomSubCategory>().UpdateAsync(newSubCtgry);
            if (isImageUpdated)
            {
                newSubCtgry.ImgNm = uniqueFileName;
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    customSubCategoryUpdDto.imgNm.CopyTo(fileStream);
                    fileStream.Close();
                }
            }
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newSubCtgry.Id;
            return response;
        }
    }
}
