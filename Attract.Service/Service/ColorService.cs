using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Cart;
using Attract.Common.DTOs.Color;
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
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ColorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ColorDTO>> GetColors()
        {
            var colors = await _unitOfWork.GetRepository<Color>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<ColorDTO>>(colors);
            return result;
        }

        public async Task<int> AddColor(AddColorDTO viewModel)
        {
            var color = new Color
            {
                Name = viewModel.Name,
                ColorHexa = viewModel.ColorHexa,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
            };
            await _unitOfWork.GetRepository<Color>().InsertAsync(color);
            await _unitOfWork.SaveChangesAsync();
            return color.Id;
        }

        public async Task<BaseCommandResponse> UpdateColor(UpdateColorDTO viewModel)
        {
            var color = await _unitOfWork.GetRepository<Color>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == viewModel.Id);
            if (color == null)
            {
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Not Found"
                };
            }

            color.Name = viewModel.Name;
            color.ColorHexa = viewModel.ColorHexa;
            color.ModifyOn = DateTime.Now;
            _unitOfWork.GetRepository<Color>().UpdateAsync(color);
                
            await _unitOfWork.SaveChangesAsync();
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Updated Successfully"
            };
        }

        public async Task<BaseCommandResponse> DeleteColor(int id)
        {
            var response = new BaseCommandResponse();
            var color = await _unitOfWork.GetRepository<Color>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (color == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = _mapper.Map<Color>(color);
            _unitOfWork.GetRepository<Color>().Delete(color.Id);
            await _unitOfWork.SaveChangesAsync();
            response.Success = true;
            return response;
        }

        public async Task<BaseCommandResponse> AddColorRange(List<AddColorDTO> addColorDTOs)
        {
            var BaseCommandResponse = new BaseCommandResponse();
            int Count = 0;
            for (int i = 0; i < addColorDTOs.Count; i++)
            {
                var color = new Color
                {
                    Name = addColorDTOs[i].Name,
                    ColorHexa = addColorDTOs[i].ColorHexa,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now,
                };
                try
                {
                    _unitOfWork.GetRepository<Color>().Insert(color);
                    await _unitOfWork.SaveChangesAsync();
                    BaseCommandResponse.Message = $" {Count++} Added Successfully ";
                }
                catch
                {
                    continue;
                }
            }
            return BaseCommandResponse;
        }
    }
}
