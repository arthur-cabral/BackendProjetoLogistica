using Application.DTO.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<CreateToken> Login(UserDTO userDTO);
        Task<CreateToken> Register(UserDTO userDTO);
    }
}
