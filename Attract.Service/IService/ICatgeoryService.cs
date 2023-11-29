using Attract.Common.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface ICatgeoryService
    {
        Task<BaseCommandResponse> GetAllCategories();
        Task<BaseCommandResponse> GetCategory(int categoryID);
    }
}
