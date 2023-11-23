using Attract.Common.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service
{
    public interface IProductTest
    {
        Task<BaseCommandResponse> GetProducts();
    }
}
