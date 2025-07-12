using System.Diagnostics.CodeAnalysis;
using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel;

namespace Application.Abstractions.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
    where TResponse : Result
{
  public async Task<TResponse> Handle(
      TRequest request,
      RequestHandlerDelegate<TResponse> next,
      CancellationToken cancellationToken)
  {
    string requestName = typeof(TRequest).Name;

    logger.LogInformation("Processing request {RequestName}", requestName);

    TResponse result = await next(cancellationToken);

    if (result.IsSuccess)
    {
      logger.LogInformation("Completed request {RequestName}", requestName);
    }
    else if (result.Error is ValidationError validationError)
    {
      logger.LogWarning("Validation error: {Description}", validationError.Description);
      logger.LogWarning("Completed request {RequestName} with validation error", requestName);
    }
    else
    {
      logger.LogError("Error: {Description}", result.Error.Description);
      logger.LogError("Completed request {RequestName} with error", requestName);
    }

    return result;
  }
}
