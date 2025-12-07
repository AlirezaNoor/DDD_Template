using FluentValidation;
using MediatR;
using DDD.Domain.Common;

namespace DDD.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
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

        var validationResults = await Task.WhenAll(
            _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (failures.Any())
        {
            // Create a Validation Error Result
            // We need to construct TResponse which is Result<T>
            // Reflection or specific construction needed since TResponse is generic
            // But strict TResponse constraint is hard.
            // Simplified: Throw ValidationException, let GlobalExceptionHandler handle it?
            // OR Return Result.Failure.
            
            // For now, let's just throw to be safe and simple, relying on Global Exception Handler
            // effectively or rewrite to use Result.Failure if TResponse is Result
            throw new ValidationException(failures);
        }

        return await next();
    }
}
