using Microsoft.EntityFrameworkCore;
using Onebrb.Server.Data;
using Onebrb.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Abstractions
{
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : class, IEntity
    {
        private readonly ApplicationDbContext _db;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<TModel> Get(int id)
        {
            return await _db.Set<TModel>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<TModel>> GetAll(int id)
        {
            var result = await _db.Set<TModel>().Where(x => x.Id == id).ToListAsync();

            if (result == null)
            {
                return Enumerable.Empty<TModel>();
            }

            return result;
        }
    }
}
