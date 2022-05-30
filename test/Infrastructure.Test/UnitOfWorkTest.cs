﻿using Domain.Abstracts.Database;
using Infrastructure.Database;
using NFluent;
using Tools.Common.Test;
using Xunit;
namespace Infrastructure.Test
{
    public sealed class UnitOfWorkTest
    {
        [Fact]
        public void Begin_should_create_new_transaction()
        {
            //arrange
            var unitOfWork = UnitOfWorkBuilder
                .Instance
                .WithConnectionFactory<DatabaseConfiguration>()
                .WithDbTransaction()
                .Build();

            //act
            unitOfWork.Begin();

            //assert
            Check.That(unitOfWork.Transaction).IsNotNull();
            Check.That(unitOfWork.Connection).IsNotNull();
        }

        [Fact]
        public void Commit_should_validate_changes_and_dispose_connection_and_transaction()
        {
            //arrange
            IUnitOfWork unitOfWork;
            using (unitOfWork = UnitOfWorkBuilder
                       .Instance
                       .WithConnectionFactory<DatabaseConfiguration>()
                       .WithDbTransaction()
                       .Build())
            {
                //act
                unitOfWork.Begin();
                unitOfWork.Commit();
            }

            //assert
            Check.That(unitOfWork.Transaction).IsNull();
            Check.That(unitOfWork.Connection).IsNull();
        }

        [Fact]
        public void Rollback_should_undo_changes_and_dispose_connection_and_transaction()
        {
            //arrange
            IUnitOfWork unitOfWork;
            using (unitOfWork = UnitOfWorkBuilder
                       .Instance
                       .WithConnectionFactory<DatabaseConfiguration>()
                       .WithDbTransaction()
                       .Build())
            {
                //act
                unitOfWork.Begin();
                unitOfWork.Rollback();
            }

            //assert
            Check.That(unitOfWork.Transaction).IsNull();
            Check.That(unitOfWork.Connection).IsNull();
        }

        [Fact]
        public void Dispose_should_delete_transaction_and_connection()
        {
            //arrange
            var unitOfWork = UnitOfWorkBuilder
                .Instance
                .WithConnectionFactory<DatabaseConfiguration>()
                .WithDbTransaction()
                .Build();

            //act
            unitOfWork.Dispose();

            //assert
            Check.That(unitOfWork.Transaction).IsNull();
            Check.That(unitOfWork.Connection).IsNull();
        }
    }
}