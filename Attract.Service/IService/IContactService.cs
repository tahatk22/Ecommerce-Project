using Attract.Common.BaseResponse;
using Attract.Common.DTOs.Contact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface IContactService
    {
        Task<BaseCommandResponse> GetAllConatcts();
        Task<BaseCommandResponse> AddConatct(AddContactDTO addContactDTO);
    }
}
