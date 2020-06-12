using Microsoft.EntityFrameworkCore;
using Onebrb.Server.Abstractions;
using Onebrb.Server.Data;
using Onebrb.Server.Interfaces;
using Onebrb.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Repos
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db) { _db = db; }

        public async Task<ApplicationUser> GetUserByUsername(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(x => x.UserName == username);
        }
    }
}
