using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Onebrb.Server.Abstractions;
using Onebrb.Server.Data;
using Onebrb.Server.Interfaces;
using Onebrb.Server.Models;
using Onebrb.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Repos
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public UserRepository(ApplicationDbContext db, IMapper mapper) : base(db)
        {
            _db = db; _mapper = mapper;
        }

        public async Task<UserDto> GetUserByUsername(string username)
        {
            var user =  await _db.Users.FirstOrDefaultAsync(x => x.UserName == username);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserById(int id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<UserDto>(user);
        }
    }
}
