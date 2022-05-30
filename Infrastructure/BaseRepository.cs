using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper.FastCrud;
using Dapper.FastCrud.Configuration.StatementOptions.Builders;
using Domain.Abstracts.Database;
namespace Infrastructure
{
    public abstract class BaseRepository<T>
    {
        private readonly IUnitOfWork _unitOfWork;
        protected BaseRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected Task Insert(T item)
        {
            return _unitOfWork.Connection.InsertAsync(item, 
                statement => statement.AttachToTransaction(_unitOfWork.Transaction));
        }

        protected Task<T> GetAsync(T item)
        {
            return _unitOfWork.Connection
                .GetAsync(item);
        }

        protected Task<IEnumerable<T>> FindAsync(Action<IRangedBatchSelectSqlSqlStatementOptionsOptionsBuilder<T>> statementOptions)
        {
            return _unitOfWork.Connection
                .FindAsync(statementOptions);
        }
    }
}