using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstracts.Database;
using MediatR;
using Moq;
using NFluent;
using Tools.Common.Test;
using Xunit;
namespace Application.Test
{
    public class UnitOfWorkCommandBehaviorTest
    {
        private readonly Mock<RequestHandlerDelegate<Unit>> _pipelineBehaviourDelegate;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        public UnitOfWorkCommandBehaviorTest()
        {
            _pipelineBehaviourDelegate = new Mock<RequestHandlerDelegate<Unit>>(MockBehavior.Strict);
            _unitOfWork = new Mock<IUnitOfWork>(MockBehavior.Strict);
        }
        
        [Fact]
        public async Task Handle_Next_Return_Ok_CallInOrder_Begin_Invoke_Commit()
        {
            //arrange
            var handlerPipeline = UnitOfWorkCommandBuilder
                .GetInstance(_pipelineBehaviourDelegate, _unitOfWork)
                .WithBeginTransaction()
                .WithCommitTransaction()
                .WithDisposeTransaction()
                .DelegateReturnResponse()
                .Build<FakeCommand>();

            //act
            await handlerPipeline
                .Handle(new FakeCommand(),
                    CancellationToken.None,
                    _pipelineBehaviourDelegate.Object);

            //assert
            _pipelineBehaviourDelegate.Verify(x => x.Invoke(), Times.Once);
            _unitOfWork.Verify(x => x.Rollback(), Times.Never);
            _unitOfWork.Verify(x => x.Dispose(), Times.Once);
            _unitOfWork.Verify(x => x.Commit(), Times.Once);
            _unitOfWork.Verify(x => x.Begin(), Times.Once);
        }

        [Fact]
        public void Handle_Next_Throw_Error_CallInOrder_Begin_Invoke_Rollback()
        {
            //arrange & act
            Check.ThatAsyncCode(async () =>
            {
                await UnitOfWorkCommandBuilder
                    .GetInstance(_pipelineBehaviourDelegate, _unitOfWork)
                    .WithBeginTransaction()
                    .WithRollbackTransaction()
                    .WithDisposeTransaction()
                    .DelegateThrowException()
                    .Build<FakeCommand>()
                    .Handle(new FakeCommand(),
                        CancellationToken.None,
                        _pipelineBehaviourDelegate.Object);
            })
            .Throws<Exception>();
            
            //assert
            _pipelineBehaviourDelegate.Verify(x => x.Invoke(), Times.Once);
            _unitOfWork.Verify(x => x.Rollback(), Times.Once);
            _unitOfWork.Verify(x => x.Dispose(), Times.Once);
            _unitOfWork.Verify(x => x.Commit(), Times.Never);
            _unitOfWork.Verify(x => x.Begin(), Times.Once);
        }
    }
}