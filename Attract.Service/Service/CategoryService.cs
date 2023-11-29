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

        public async Task<BaseCommandResponse> AddCategory(CategoryAddDto categoryAddDto)
        {
            var response = new BaseCommandResponse();
            var newCtgry = mapper.Map<Category>(categoryAddDto);
            //var newProduct = mapper.Map<Product>(addProductDTO);
            await unitOfWork.GetRepository<Category>().InsertAsync(newCtgry);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newCtgry.Id;
            return response;
        }
        public async Task<BaseCommandResponse> UpdCategory(CategoryUpdDto categoryUpdDto)
        {
            var response = new BaseCommandResponse();
            // Retrieve the existing category from the database
            var existingCategory = await unitOfWork.GetRepository<Category>().GetFirstOrDefaultAsync(predicate: x => x.Id == categoryUpdDto.Id);

            if (existingCategory == null)
            {
                response.Success = false;
                response.Message = "Category not found.";
                return response;
            }
            var newCtgry = mapper.Map<Category>(categoryUpdDto);
            unitOfWork.GetRepository<Category>().UpdateAsync(newCtgry);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newCtgry.Id;
            return response;
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
