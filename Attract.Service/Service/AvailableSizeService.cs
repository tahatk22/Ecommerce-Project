using Attract.Common.BaseResponse;
using Attract.Common.DTOs.AvailableSize;
using Attract.Common.DTOs.Color;
using Attract.Common.DTOs.CustomSubCategory;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class AvailableSizeService : IAvailableSizeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AvailableSizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AvailableSizeDTO>> GetAvailableSizes()
        {
            var availableSizes = await _unitOfWork.GetRepository<AvailableSize>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<AvailableSizeDTO>>(availableSizes);
            return result;
        }

        public async Task<int> AddAvailableSize(AddAvailableSizeDTO viewModel)
        {
            var availableSize = new AvailableSize
            {
                Name = viewModel.Name,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
            };
            await _unitOfWork.GetRepository<AvailableSize>().InsertAsync(availableSize);
            await _unitOfWork.SaveChangesAsync();
            return availableSize.Id;
        }

        public async Task<BaseCommandResponse> UpdateAvailableSize(UpdateAvailableSizeDTO viewModel)
        {
            var availableSize = await _unitOfWork.GetRepository<AvailableSize>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == viewModel.Id);
            if (availableSize == null)
            {
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Not Found"
                };
            }

            availableSize.Name = viewModel.Name;
            availableSize.ModifyOn = DateTime.Now;
            _unitOfWork.GetRepository<AvailableSize>().UpdateAsync(availableSize);
                
            await _unitOfWork.SaveChangesAsync();
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Updated Successfully"
            };
        }

        public async Task<BaseCommandResponse> DeleteAvailableSize(int id)
        {
            var response = new BaseCommandResponse();
            var size = await _unitOfWork.GetRepository<AvailableSize>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (size == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = _mapper.Map<AvailableSize>(size);
            _unitOfWork.GetRepository<AvailableSize>().Delete(size.Id);
            await _unitOfWork.SaveChangesAsync();
            response.Success = true;
            return response;
        }
    }
}
