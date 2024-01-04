using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Collection;
using Attract.Common.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ICollectionService
    {
        Task<BaseCommandResponse> AddCollection(AddCollectionDTO addCollectionDTO);
        Task<BaseCommandResponse> GetAllCollections(PagingParams pagingParams);
    }
}
