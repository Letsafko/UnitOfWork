using System;
using System.Data;
namespace Domain.Abstracts.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IDbTransaction Transaction { get; }
        IDbConnection Connection { get; }
        void Rollback();
        void Commit();
        void Begin();
    }
}