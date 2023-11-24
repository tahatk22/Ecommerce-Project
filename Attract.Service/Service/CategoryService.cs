using Attract.Common.BaseResponse;
using Attract.Domain.Entities.Attract;
using Attract.Framework.UoW;
using Attract.Service.IService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class CategoryService : ICatgeoryService
    {
        private readonly IUnitOfWork unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<BaseCommandResponse> GetAllCategories()
        {
            var response = new BaseCommandResponse();
            var categories=await unitOfWork.GetRepository<Category>().GetAllAsync();
            response.Success=true;
            response.Data=categories;
            return response;
        }
    }
}
