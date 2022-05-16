using System;
using System.Data;
using Domain.Abstracts;

namespace Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        #region fields & ctor

        public UnitOfWork(IConnectionFactory connectionFactory)
        {
            Connection = connectionFactory.CreateConnection<DatabaseConfiguration>();
        }
        
        ~UnitOfWork() => Dispose();

        private bool _disposed;

        #endregion

        public IDbTransaction Transaction { get; private set; }
        public IDbConnection Connection { get; private set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Rollback()
        {
            Transaction.Rollback();
        }

        public void Commit()
        {
            Transaction.Commit();
        }

        public void Begin()
        {
            Transaction = Connection.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        private void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                Transaction?.Dispose();
                Connection?.Dispose();
                Transaction = null;
                Connection = null;
            }

            _disposed = true;
        }
    }
}