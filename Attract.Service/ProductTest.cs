using Attract.Common.BaseResponse;
using Attract.Domain.Entities.Attract;
using Attract.Framework.UoW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service
{
    public class ProductTest : IProductTest
    {
        private readonly IUnitOfWork unitOfWork;

        public ProductTest(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> GetProducts()
        {
            var response = new BaseCommandResponse();
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync();
            if(products == null)
            {
                response.Success = false;
                return response;
            }
            response.Success = true;
            response.Data=products;
            return response;
        }
    }
}
