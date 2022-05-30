using System;
using Application.Behaviors;
using Domain.Abstracts.Database;
using Domain.Abstracts.Mediator;
using Domain.Abstracts.Mediator.Command;
using MediatR;
using Moq;
namespace Tools.Common.Test
{
    public sealed class UnitOfWorkCommandBuilder
    {
        private readonly Mock<RequestHandlerDelegate<Unit>> _pipelineBehaviourDelegate;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private UnitOfWorkCommandBuilder(Mock<RequestHandlerDelegate<Unit>> pipelineBehaviourDelegate,
            Mock<IUnitOfWork> unitOfWork)
        {
            _pipelineBehaviourDelegate = pipelineBehaviourDelegate;
            _unitOfWork = unitOfWork;
        }

        public static UnitOfWorkCommandBuilder GetInstance(Mock<RequestHandlerDelegate<Unit>> pipelineBehaviourDelegate,
            Mock<IUnitOfWork> unitOfWork) => new UnitOfWorkCommandBuilder(pipelineBehaviourDelegate,
            unitOfWork);
        
        public UnitOfWorkCommandBehavior<TCommand> Build<TCommand>()
            where TCommand : ICommand
        {
            return new UnitOfWorkCommandBehavior<TCommand>(_unitOfWork.Object);
        }

        public UnitOfWorkCommandBuilder WithCommitTransaction()
        {
            _unitOfWork.Setup(x => x.Commit())
                .Verifiable();
            
            return this;
        }
        
        public UnitOfWorkCommandBuilder WithRollbackTransaction()
        {
            _unitOfWork.Setup(x => x.Rollback())
                .Verifiable();
            
            return this;
        }
        
        public UnitOfWorkCommandBuilder WithDisposeTransaction()
        {
            _unitOfWork.Setup(x => x.Dispose())
                .Verifiable();
            
            return this;
        }
        
        public UnitOfWorkCommandBuilder WithBeginTransaction()
        {
            _unitOfWork.Setup(x => x.Begin())
                .Verifiable();

            return this;
        }
        
        public UnitOfWorkCommandBuilder DelegateThrowException()
        {
            _pipelineBehaviourDelegate.Setup(x => x.Invoke())
                .Throws<Exception>()
                .Verifiable();
            
            return this;
        }

        public UnitOfWorkCommandBuilder DelegateReturnResponse()
        {
            _pipelineBehaviourDelegate.Setup(x =>x.Invoke())
                .ReturnsAsync(Unit.Value)
                .Verifiable();
            
            return this;
        }
    }
}