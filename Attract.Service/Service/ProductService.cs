using Attract.Common.BaseResponse;
using Attract.Common.DTOs;
using Attract.Domain.Entities.Attract;
using Attract.Framework.UoW;
using Attract.Service.IService;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> GetAllSubCategoryProducts(int subCategoryId)
        {
            var response = new BaseCommandResponse();
            var products = await unitOfWork.GetRepository<Product>().GetAllAsync(s => s.SubCategoryId == subCategoryId,include: s => s.Include(p => p.Colors));
            if (products == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<IList<ProductDTO>>(products);
            response.Success = true;
            response.Data = result;
            return response;

        }
    }
}
