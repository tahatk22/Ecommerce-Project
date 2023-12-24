using Attract.Common.BaseResponse;
using Attract.Common.DTOs;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.SubCategory;
using Attract.Domain.Entities.Attract;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AttractDomain.Entities.Attract;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class SubCategoryService : ISubCategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public SubCategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> AddSubCategory(SubCategoryAddDto subCategoryAddDto)
        {
            var response = new BaseCommandResponse();
            var newSubCtgry = mapper.Map<SubCategory>(subCategoryAddDto);
            newSubCtgry.CreatedBy = 1;
            await unitOfWork.GetRepository<SubCategory>().InsertAsync(newSubCtgry);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newSubCtgry.Id;
            return response;
        }

        public async Task<BaseCommandResponse> DeleteSubCategory(int id)
        {
            var response = new BaseCommandResponse();
            var subCategory = await unitOfWork.GetRepository<SubCategory>().GetFirstOrDefaultAsync(predicate: x => x.Id == id);
            if (subCategory == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<SubCategory>(subCategory);
            unitOfWork.GetRepository<SubCategory>().Delete(subCategory.Id);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            return response;
        }

        public async Task<BaseCommandResponse> GetAllSubCategories()
        {

            var response = new BaseCommandResponse();
            var subCategories = await unitOfWork.GetRepository<SubCategory>()
                .GetAllAsync(include: s => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(s, p => p.Products));
            if (subCategories == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<IList<SubCategoryDto>>(subCategories);
            response.Success = true;
            response.Data = result;
            return response;
        }

        public async Task<BaseCommandResponse> GetSubCategory(int subCategoryId)
        {
            var response = new BaseCommandResponse();
            var subCategory = await unitOfWork.GetRepository<SubCategory>().GetFirstOrDefaultAsync(predicate: x => x.Id == subCategoryId, include: s => Microsoft.EntityFrameworkCore.EntityFrameworkQueryableExtensions.Include(s, p => p.Products));
            if (subCategory == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<SubCategoryDto>(subCategory);
            response.Success = true;
            response.Data = result;
            return response;

        }

        public async Task<BaseCommandResponse> UpdSubCategory(SubCategoryUpdDto subCategoryUpdDto)
        {
            var response = new BaseCommandResponse();
            // Retrieve the existing category from the database
            var existingSubCategory = await unitOfWork.GetRepository<SubCategory>().GetFirstOrDefaultAsync(predicate: x => x.Id == subCategoryUpdDto.Id);

            if (existingSubCategory == null)
            {
                response.Success = false;
                response.Message = "Category not found.";
                return response;
            }
            var newSubCtgry = mapper.Map<SubCategory>(subCategoryUpdDto);
            newSubCtgry.CreatedBy = existingSubCategory.CreatedBy;
            newSubCtgry.CreatedOn = existingSubCategory.CreatedOn;
            unitOfWork.GetRepository<SubCategory>().UpdateAsync(newSubCtgry);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newSubCtgry.Id;
            return response;
        }
    }
}
