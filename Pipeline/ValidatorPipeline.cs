using FluentValidation;
using MediatR;

namespace BlazorWebApp.Pipeline;

public class ValidatorPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidatorPipeline(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failuresTasks = _validators
            .Select(async x => await x.ValidateAsync(context, cancellationToken))
            .ToList();
        
        await Task.WhenAll(failuresTasks);
        
        var failures = failuresTasks
            .SelectMany(x => x.Result.Errors)
            .Where(x => x != null)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(failures);
        }

        return await next();
    }
}