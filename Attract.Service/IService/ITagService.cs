using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Tag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ITagService
    {
        Task<IEnumerable<TagDTO>> GetTags();
        Task<int> AddTag(AddTagDTO viewModel);
        Task<BaseCommandResponse> UpdateTag(UpdateTagDTO viewModel);
    }
}
