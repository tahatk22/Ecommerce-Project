using Attract.Common.BaseResponse;
using Attract.Common.DTOs.SubCategory;
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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public Task<BaseCommandResponse> AddOredr(SubCategoryAddDto subCategoryAddDto)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseCommandResponse> GetAllOredrs()
        {
            var response = new BaseCommandResponse();
            var orders = await unitOfWork.GetRepository<Order>().GetAllAsync();
            if (orders == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<IList<SubCategoryDto>>(orders);
            response.Success = true;
            response.Data = result;
            return response;
        }

        public Task<BaseCommandResponse> GetOredr(int ordrId)
        {
            throw new NotImplementedException();
        }
    }
}
