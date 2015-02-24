using System.Collections.Generic;
using System.Data.Entity;

namespace Auctions.Export
{
    public class Updater<T> : IUpdater<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        public Updater(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                _dbContext.Set<T>().Add(entity);
                _dbContext.SaveChanges();
            }
        }
    }
}