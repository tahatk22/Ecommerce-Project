using Attract.Common.BaseResponse;
using Attract.Common.DTOs.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface IOrderService
    {
        Task<BaseCommandResponse> GetAllOredrs();
        Task<BaseCommandResponse> GetOredr(int ordrId);
        Task<BaseCommandResponse> AddOredr(SubCategoryAddDto subCategoryAddDto);
    }
}
