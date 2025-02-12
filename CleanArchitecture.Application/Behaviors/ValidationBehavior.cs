using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace CleanArchitecture.Application.Behaviors
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
            {
                return await next();
            }

            var context = new ValidationContext<TRequest>(request);
            var errorsDictionary = _validators
                .Select(v => v.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .GroupBy(
                failure => failure.PropertyName,
                failure => failure.ErrorMessage, (PropertyName, ErrorMessage)=> new
                {
                    Key = PropertyName,
                    Values = ErrorMessage.Distinct().ToList()
                }).ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.Values[0]);

            if (errorsDictionary.Any())
            {
                var errors = errorsDictionary.Select(failure => new ValidationFailure
                {
                    PropertyName = failure.Value,
                    ErrorCode = failure.Key
                });
                throw new ValidationException(errors);

            }

            return await next();

        }
    }
}
