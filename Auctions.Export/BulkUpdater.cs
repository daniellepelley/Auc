using System.Collections.Generic;
using System.Data.Entity;
using System.Transactions;
using EntityFramework.BulkInsert.Extensions;

namespace Auctions.Export
{
    public class BulkUpdater<T> : IUpdater<T>
        where T : class
    {
        private readonly DbContext _dbContext;

        public BulkUpdater(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IEnumerable<T> entities)
        {
            using (var transactionScope = new TransactionScope())
            {
                _dbContext.BulkInsert(entities);

                _dbContext.SaveChanges();
                transactionScope.Complete();
            }

            _dbContext.SaveChanges();
        }
    }
}