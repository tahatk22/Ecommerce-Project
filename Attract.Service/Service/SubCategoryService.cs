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
        public Task<BaseCommandResponse> AddSubCategory(CategoryAddDto categoryAddDto)
        {
            throw new NotImplementedException();
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

        public Task<BaseCommandResponse> UpdSubCategory(CategoryUpdDto categoryUpdDto)
        {
            throw new NotImplementedException();
        }
    }
}
