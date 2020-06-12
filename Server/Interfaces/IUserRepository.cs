using Onebrb.Server.Models;
using Onebrb.Shared.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<UserDto> GetUserByUserName(string username);
        Task<UserDto> GetUserById(int id);
    }
}
