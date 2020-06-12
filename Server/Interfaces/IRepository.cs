﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Interfaces
{
    /// <summary>
    /// Basic repository interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> Get(int id);
        Task<IEnumerable<TEntity>> GetAll(int id);
    }
}
