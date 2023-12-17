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
    }
}
