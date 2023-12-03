using Attract.Common.BaseResponse;
using Attract.Common.DTOs.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Attract.Service.IService
{
    public interface IAuthService
    {
        Task<BaseCommandResponse> Register(UserDTO userDTO);
        Task<BaseCommandResponse> Login(LoginUserDTO loginUserDTO);
        string GetCurrentUserId();
        
    }
}
