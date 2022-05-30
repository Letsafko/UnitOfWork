using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Abstracts.Database;
using Domain.Abstracts.Mediator.Command;
using MediatR;
namespace Application.Behaviors
{
    public sealed class UnitOfWorkCommandBehavior<TCommand> : IPipelineBehavior<TCommand, Unit>
        where TCommand : ICommand
    {
        private readonly IUnitOfWork _unitOfWork;
        public UnitOfWorkCommandBehavior(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(TCommand request, CancellationToken cancellationToken, RequestHandlerDelegate<Unit> next)
        {
            _unitOfWork.Begin();
            try
            {
                var result = await next();
                _unitOfWork.Commit();
                return result;
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
            finally
            {
                _unitOfWork.Dispose();
            }
        }
    }
}