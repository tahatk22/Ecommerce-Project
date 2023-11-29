using Attract.Common.BaseResponse;
using Attract.Common.DTOs;
using Attract.Domain.Entities.Attract;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AutoMapper;
using System.Data.Entity;

namespace Attract.Service.Service
{
    public class CategoryService : ICatgeoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public CategoryService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> GetAllCategories()
        {
            var response = new BaseCommandResponse();
            var categories = await unitOfWork.GetRepository<Category>()
                .GetAllAsync(include: s => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(s, p => p.SubCategories));
            if (categories == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<IList<CategoryDto>>(categories);
            response.Success = true;
            response.Data = result;
            return response;
        }

        public async Task<BaseCommandResponse> GetCategory(int categoryID)
        {
            var response = new BaseCommandResponse();
            var ctgry = await unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(predicate: x => x.Id == categoryID,include: s => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(s, p => p.SubCategories));
            if (ctgry == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<CategoryDto>(ctgry);
            response.Success = true;
            response.Data = result;
            return response;
        }
    }
}
