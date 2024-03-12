using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Collection;
using Attract.Common.DTOs.Country;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.Helpers;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Attract.Service.Service
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;

        public CountryService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> AddCountry(AddCountryDTO addCountryDTO)
        {
            var response = new BaseCommandResponse();
            if (addCountryDTO.Name == null || addCountryDTO.CountryFlag == null)
            {
                response.Success= false;
                response.Message = "Please Add Vaild Image and Name";
                return response;
            }
            var productDirectoryPath = GetProductDirectoryPath();
            if (!Directory.Exists(productDirectoryPath))
            {
                Directory.CreateDirectory(productDirectoryPath);
            }
            await SaveImageAsync(productDirectoryPath, addCountryDTO.CountryFlag);
            var newCountry = new Country
            {
                Name = addCountryDTO.Name,
                CountryFlag = Guid.NewGuid().ToString() + "_" + addCountryDTO.CountryFlag.FileName
            };
            await unitOfWork.GetRepository<Country>().InsertAsync(newCountry);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newCountry.Id;
            return response;
        }

        public async Task<BaseCommandResponse> GetAllCountries()
        {
            var response = new BaseCommandResponse();
            var countries = unitOfWork.GetRepository<Country>().GetAll();
            if (countries == null)
            {
                response.Success = true;
                response.Message = "No Collections Found";
                return response;
            }
            var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;
            var result = mapper.Map<IList<CountryDto>>(countries);
            foreach (var item in result)
            {
                //Get the host value
                var LastUnderScore = item.CountryFlag.LastIndexOf('_');
                var RealImageName = item.CountryFlag.Substring(LastUnderScore + 1);
                item.CountryFlag = $"http://{hostValue}/Images/customsubcategory/{RealImageName}";
            }
            response.Data = result;
            response.Success = true;
            return response;
        }


        #region Private methods
        private string GetProductDirectoryPath()
        {
            return Path.Combine("wwwroot", "Images", "Countries");
        }

        private async Task SaveImageAsync(string directoryPath, IFormFile image)
        {
            var imagePath = Path.Combine(directoryPath, image.FileName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
        }
        private async Task DeleteImageAsync(string ImageName , string directoryPath)
        {
            var LastUnderScore = ImageName.LastIndexOf('_');
            var RealImageName = ImageName.Substring(LastUnderScore + 1);
            var ImageToBeDeleted = Path.Combine(directoryPath, RealImageName);
            File.Delete(ImageToBeDeleted);
        }
        private static string GetFullImagePath(string hostValue, string imageName)
        {
            // Construct the full image path based on your application's logic
            // For example, if images are stored in a specific directory:
            return $"http://{hostValue}/Images/Countries/{imageName}";
        }

        public async Task<BaseCommandResponse> UpdateCountry(UpdateCountryDto updateCountryDto)
        {
            var response = new BaseCommandResponse();
            var result = await unitOfWork.GetRepository<Country>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == updateCountryDto.Id);
            if (result == null)
            {
                response.Success = false;
                response.Message = "Contact not found.";
                return response;
            }
            if (updateCountryDto.CountryFlag != null)
            {
                var productDirectoryPath = GetProductDirectoryPath();
                if (!Directory.Exists(productDirectoryPath))
                {
                    Directory.CreateDirectory(productDirectoryPath);
                }
                await DeleteImageAsync(result.CountryFlag, productDirectoryPath);
                await SaveImageAsync(productDirectoryPath, updateCountryDto.CountryFlag);
                result.Name = updateCountryDto.Name;
                result.CountryFlag = Guid.NewGuid().ToString() + "_" + updateCountryDto.CountryFlag.FileName;
            }
            else
            {
                var CurrentCountry = await unitOfWork.GetRepository<Country>().GetFirstOrDefaultAsync(predicate: x => x.Id == updateCountryDto.Id);
                result.Name = updateCountryDto.Name; 
                result.CountryFlag = CurrentCountry.CountryFlag;
            }
            unitOfWork.GetRepository<Country>().UpdateAsync(result);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = result.Id;
            return response;
        }

        public async Task<BaseCommandResponse> DeleteCountry(int id)
        {
            var response = new BaseCommandResponse();
            var result = await unitOfWork.GetRepository<Country>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (result == null) 
            {
                response.Success = false;
                response.Message = "Contact not found.";
                return response;
            }
            var directorypath = GetProductDirectoryPath();
            await DeleteImageAsync(result.CountryFlag, directorypath);
            unitOfWork.GetRepository<Country>().Delete(id);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            return response;
        }
        #endregion
    }
}
