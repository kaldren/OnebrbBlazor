﻿using Onebrb.Server.Models;
using Onebrb.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<UserDto> GetUserByUsername(string username);
        Task<UserDto> GetUserById(int id);
    }
}
