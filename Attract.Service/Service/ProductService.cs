using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Product;
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

        public async Task<BaseCommandResponse> AddProduct(AddProductDTO addProductDTO)
        {
            var response=new BaseCommandResponse();
            var newProduct=mapper.Map<Product>(addProductDTO);
            await unitOfWork.GetRepository<Product>().InsertAsync(newProduct);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Data = newProduct.Id;
            return response;
        }

        public async Task<BaseCommandResponse> GetAllSubCategoryProducts(int subCategoryId)
        {
            var response = new BaseCommandResponse();

            try
            {
                var products = await unitOfWork.GetRepository<Product>()
                    .GetAllAsync(
                        s => s.SubCategoryId == subCategoryId,
                        include: s => s
                            .Include(p => p.ProductAvailableSizes)
                            .ThenInclude(pas => pas.AvailableSize)
                            .Include(p => p.ProductColors)
                            .ThenInclude(pc => pc.Color)
                            .Include(p=>p.OrderDetails)
                    );

                if (products == null || !products.Any())
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
            catch (Exception ex)
            {
                // Handle exceptions and log if necessary
                response.Success = false;
                response.Message = "An error occurred while fetching products.";
                return response;
            }
        }
    }
}
