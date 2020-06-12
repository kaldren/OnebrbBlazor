using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Onebrb.Server.Interfaces
{
    /// <summary>
    /// Basic repository interface
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IRepository<TModel> where TModel : class, IEntity
    {
        Task<TModel> Get(int id);
    }
}
