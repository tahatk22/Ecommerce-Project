using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Collection;
using Attract.Common.DTOs.Country;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Common.DTOs.Slider;
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
    public class SliderService : ISliderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IMapper mapper;
        private bool sliderVal = false;
        public SliderService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> AddSlider(AddSliderDto addSliderDto)
        {
            var response = new BaseCommandResponse();
            if (addSliderDto == null)
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
            await SaveImageAsync(productDirectoryPath, addSliderDto.Image);
            var newSlider = new Slider
            {
                Title = addSliderDto.Title,
                Image = Guid.NewGuid().ToString() + "_" + addSliderDto.Image.FileName 
            };
            await unitOfWork.GetRepository<Slider>().InsertAsync(newSlider);
            await unitOfWork.SaveChangesAsync();
            var sliderVal = await unitOfWork.GetRepository<SliderValue>().GetFirstOrDefaultAsync();
            if (sliderVal == null)
            {
                var SliderValue = new SliderValue
                {
                    Value = true
                };
                await unitOfWork.GetRepository<SliderValue>().InsertAsync(SliderValue);
                await unitOfWork.SaveChangesAsync();
            }
            response.Success = true;
            response.Data = newSlider.Id;
            return response;
        }

        public async Task<BaseCommandResponse> GetAllSlider()
        {
            var response = new BaseCommandResponse();
            var sliders = unitOfWork.GetRepository<Slider>().GetAll();
            if (sliders == null)
            {
                response.Success = true;
                response.Message = "No Collections Found";
                return response;
            }
            var hostValue = httpContextAccessor.HttpContext.Request.Host.Value;
            var result = mapper.Map<IList<SliderDto>>(sliders);
            foreach (var item in result)
            {
                //Get the host value
                var LastUnderScore = item.Image.LastIndexOf('_');
                var RealImageName = item.Image.Substring(LastUnderScore + 1);
                item.Image = $"http://{hostValue}/Images/Slider/{RealImageName}";
            }
            response.Data = result;
            response.Success = true;
            return response;
        }


        #region Private methods
        private string GetProductDirectoryPath()
        {
            return Path.Combine("wwwroot", "Images", "Slider");
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

        public async Task<BaseCommandResponse> SetSliderVal(bool sliderValue)
        {
            var response = new BaseCommandResponse();
            var sliderVal = await unitOfWork.GetRepository<SliderValue>().GetFirstOrDefaultAsync();
            if (sliderVal == null)
            {
                response.Success = true;
                response.Message = "No Slider Value Found";
                return response;
            }
            sliderVal.Value = sliderValue;
            unitOfWork.GetRepository<SliderValue>().UpdateAsync(sliderVal);
            await unitOfWork.SaveChangesAsync();
            response.Data = sliderVal;
            response.Success = true;
            return response;

        }

        public async Task<BaseCommandResponse> GetCurrentSliderValue()
        {
            var response = new BaseCommandResponse();
            var sliderVal = await unitOfWork.GetRepository<SliderValue>().GetFirstOrDefaultAsync();
            if (sliderVal == null)
            {
                response.Success = true;
                response.Message = "No Slider Value Found";
                return response;
            }
            response.Data = sliderVal;
            response.Success = true;
            return response;
        }
        private async Task DeleteImageAsync(string ImageName, string directoryPath)
        {
            var LastUnderScore = ImageName.LastIndexOf('_');
            var RealImageName = ImageName.Substring(LastUnderScore + 1);
            var ImageToBeDeleted = Path.Combine(directoryPath, RealImageName);
            File.Delete(ImageToBeDeleted);
        }
        public async Task<BaseCommandResponse> UpdateSlider(UpdateSliderDto updateSliderDto)
        {
            var response = new BaseCommandResponse();
            var result = await unitOfWork.GetRepository<Slider>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == updateSliderDto.Id);
            if (result == null)
            {
                response.Success = false;
                response.Message = "Contact not found.";
                return response;
            }
            if (updateSliderDto.Image != null)
            {
                var productDirectoryPath = GetProductDirectoryPath();
                if (!Directory.Exists(productDirectoryPath))
                {
                    Directory.CreateDirectory(productDirectoryPath);
                }
                await DeleteImageAsync(result.Image, productDirectoryPath);
                await SaveImageAsync(productDirectoryPath, updateSliderDto.Image);
                result.Title = updateSliderDto.Title;
                result.Image = Guid.NewGuid().ToString() + "_" + updateSliderDto.Image.FileName;
            }
            else
            {
                var CurrentSlider = await unitOfWork.GetRepository<Slider>().GetFirstOrDefaultAsync(predicate: x => x.Id == updateSliderDto.Id);
                result.Title = updateSliderDto.Title;
                result.Image = CurrentSlider.Image;
            }
            unitOfWork.GetRepository<Slider>().UpdateAsync(result);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = result.Id;
            return response;
        }

        public async Task<BaseCommandResponse> DeleteSlider(int id)
        {
            var response = new BaseCommandResponse();
            var result = await unitOfWork.GetRepository<Slider>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (result == null)
            {
                response.Success = false;
                response.Message = "Contact not found.";
                return response;
            }
            var directorypath = GetProductDirectoryPath();
            await DeleteImageAsync(result.Image, directorypath);
            unitOfWork.GetRepository<Slider>().Delete(id);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            return response;
        }
        #endregion
    }
}
