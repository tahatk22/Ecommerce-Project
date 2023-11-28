using Attract.Common.BaseResponse;
using Attract.Domain.Entities.Attract;
using Attract.Framework.UoW;
using Attract.Service.IService;

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
            var categories = await unitOfWork.GetRepository<Category>()
                .GetAllAsync(include: s => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(s, p => p.SubCategories));
            response.Success = true;
            response.Data = categories;
            return response;
        }

    }
}
