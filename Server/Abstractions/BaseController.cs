using Microsoft.AspNetCore.Mvc;
using Onebrb.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Abstractions
{
    public abstract class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;

        public BaseController(TRepository repository)
        {
            _repository = repository;
        }

        // GET: api/[controller]
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id)
        {
            return await _repository.Get(id);
        }
    }
}
