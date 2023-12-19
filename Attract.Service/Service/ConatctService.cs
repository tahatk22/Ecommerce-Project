using Attract.Common.BaseResponse;
using Attract.Common.DTOs;
using Attract.Common.DTOs.Category;
using Attract.Common.DTOs.Contact;
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
    public class ConatctService : IContactService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public ConatctService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<BaseCommandResponse> AddConatct(AddContactDTO addContactDTO)
        {
            var response = new BaseCommandResponse();
            var newContact = mapper.Map<Contact>(addContactDTO);
            await unitOfWork.GetRepository<Contact>().InsertAsync(newContact);
            await unitOfWork.SaveChangesAsync();
            response.Success = true;
            response.Message = "Created Successfully";
            response.Data = newContact.Id;
            return response;
        }

        public async Task<BaseCommandResponse> GetAllConatcts()
        {
            var response = new BaseCommandResponse();
            var contacts = await unitOfWork.GetRepository<Contact>().GetAllAsync();
            if (contacts == null)
            {
                response.Success = false;
                response.Message = "Not Found";
                return response;
            }
            var result = mapper.Map<IList<ContactDTO>>(contacts);
            response.Success = true;
            response.Data = result;
            return response;
        }
    }
}
