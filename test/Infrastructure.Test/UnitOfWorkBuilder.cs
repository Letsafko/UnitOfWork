using System.Data;
using Domain.Abstracts.Database;
using Infrastructure;
using Infrastructure.Database;
using Moq;

namespace Tools.Common.Test
{
    public sealed class UnitOfWorkBuilder
    {
        private readonly Mock<IConnectionFactory> _connectionFactory;
        private readonly Mock<IDbTransaction> _transaction;
        private readonly Mock<IDbConnection> _connection;

        private UnitOfWorkBuilder()
        {
            _connectionFactory = new Mock<IConnectionFactory>(MockBehavior.Strict);
            _transaction = new Mock<IDbTransaction>();
            _connection = new Mock<IDbConnection>();
        }

        public static UnitOfWorkBuilder Instance => new UnitOfWorkBuilder();

        public IUnitOfWork Build()
        {
            return new UnitOfWork(_connectionFactory.Object);
        }

        public UnitOfWorkBuilder WithConnectionFactory<T>() where T : class, IDatabaseConfiguration, new()
        {
            _connectionFactory
                .Setup(x => x.CreateConnection<T>())
                .Returns(_connection.Object);

            return this;
        }

        public UnitOfWorkBuilder WithDbTransaction()
        {
            _connection
                .Setup(x => x.BeginTransaction(IsolationLevel.ReadCommitted))
                .Returns(_transaction.Object);

            return this;
        }
    }
}