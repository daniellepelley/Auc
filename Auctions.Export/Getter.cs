using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Auctions.Export
{
    public class Getter<T>
        where T : class
    {
        private readonly List<T> _existingModels;
        private readonly DbContext _dbContext;

        public Getter(DbContext dbContext, IEnumerable<T> existingModels)
        {
            _dbContext = dbContext;
            _existingModels = existingModels.ToList();
        }

        public T Get(Func<T, bool> filterFunc, Func<T> factoryAction)
        {
            var entity = _existingModels.FirstOrDefault(filterFunc);

            if (entity != null)
            {
                return entity;
            }

            entity = factoryAction();
            _dbContext.Set<T>().Add(entity);
            _existingModels.Add(entity);
            return entity;
        }
    }
}