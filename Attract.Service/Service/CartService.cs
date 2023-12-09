using Attract.Common.BaseResponse;
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
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CartService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> GetCartByUser(string userId)
        {
            int cartId = 0;
            var cart = await _unitOfWork.GetRepository<Cart>().GetFirstOrDefaultAsync(predicate: x => x.UserId == userId);
            if (cart == null)
            {
                cartId = await AddCart(userId);
            }
            else
            {
                cartId = cart.Id;
            }
            return cartId;
        }

        private async Task<int> AddCart(string userId)
        {
            var cart = new Cart
            {
                UserId = userId,
                CreatedBy = 1
            };
            await _unitOfWork.GetRepository<Cart>().InsertAsync(cart);
            await _unitOfWork.SaveChangesAsync();
            return cart.Id;
        }
    }
}
