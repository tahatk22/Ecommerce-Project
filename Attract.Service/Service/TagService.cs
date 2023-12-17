using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Cart;
using Attract.Common.DTOs.Tag;
using Attract.Domain.Entities.Attract;
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
    public class TagService : ITagService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TagService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TagDTO>> GetTags()
        {
            var tags = await _unitOfWork.GetRepository<Tag>().GetAllAsync();
            var result = _mapper.Map<IEnumerable<TagDTO>>(tags);
            return result;
        }

        public async Task<int> AddTag(AddTagDTO viewModel)
        {
            var tag = new Tag
            {
                Name = viewModel.Name,
                CreatedBy = 1,
                CreatedOn = DateTime.Now,
            };
            await _unitOfWork.GetRepository<Tag>().InsertAsync(tag);
            await _unitOfWork.SaveChangesAsync();
            return tag.Id;
        }

        public async Task<BaseCommandResponse> UpdateTag(UpdateTagDTO viewModel)
        {
            var tag = await _unitOfWork.GetRepository<Tag>()
                .GetFirstOrDefaultAsync(predicate: x => x.Id == viewModel.Id);
            if (tag == null)
            {
                return new BaseCommandResponse
                {
                    Success = true,
                    Message = "Not Found"
                };
            }

            tag.Name = viewModel.Name;
            tag.ModifyOn = DateTime.Now;
            _unitOfWork.GetRepository<Tag>().UpdateAsync(tag);
                
            await _unitOfWork.SaveChangesAsync();
            return new BaseCommandResponse
            {
                Success = true,
                Message = "Updated Successfully"
            };
        }
    }
}
