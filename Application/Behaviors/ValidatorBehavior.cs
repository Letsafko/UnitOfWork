using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using FluentValidation;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
namespace Application.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _validatorFactory;
        private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

        public ValidationBehavior(IValidatorFactory validatorFactory, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
        {
            _validatorFactory = validatorFactory;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validator = _validatorFactory.GetValidator(typeof(TRequest));
            if (validator != null)
            {
                var typeName = request.GetType().Name;
                _logger.LogDebug("validating {@Request}", request);

                var result = await validator.ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken);
                if (result?.IsValid == false)
                {
                    var failures = result.Errors;
                    _logger.LogError("validation errors - {@TypeName} - request: {@Request} - errors: {@Failures}", typeName, request, failures);
                    throw new InvalidOperationException(JsonConvert.SerializeObject(failures));
                }

                _logger.LogDebug("{ @TypeName} validated", typeName);
            }

            return await next().ConfigureAwait(false);
        }
    }
}