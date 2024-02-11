using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Collection;
using Attract.Common.DTOs.Country;
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

namespace Attract.Service.Service
{
    public class CountryService : ICountryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CountryService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
        }
        public async Task<BaseCommandResponse> AddCountry(AddCountryDTO addCountryDTO)
        {
            var response = new BaseCommandResponse();
            if (addCountryDTO == null)
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
                CountryFlag = addCountryDTO.CountryFlag.FileName
            };
            await unitOfWork.GetRepository<Country>().InsertAsync(newCountry);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newCountry.Id;
            return response;
        }

        public async Task<BaseCommandResponse> GetAllCountries(PagingParams pagingParams)
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
            var transformedCountries = countries.Select(country => new Country { Name = country.Name,
            CountryFlag = GetFullImagePath(hostValue, country.CountryFlag)
            });
            var PagedCenter = await PagedList<Country>.CreateAsync(transformedCountries, pagingParams.PageNumber, pagingParams.PageSize);
            response.Data = new Pagination<Country>(PagedCenter.CurrentPage, PagedCenter.PageSize, PagedCenter.TotalCount, PagedCenter);
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
        private static string GetFullImagePath(string hostValue, string imageName)
        {
            // Construct the full image path based on your application's logic
            // For example, if images are stored in a specific directory:
            return $"http://{hostValue}/Images/Countries/{imageName}";
        }
        #endregion
    }
}
