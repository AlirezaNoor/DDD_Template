using Microsoft.Extensions.Logging;

namespace DDD.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        
        _logger.LogInformation("Handling {RequestName} at {DateTime}", 
            requestName, DateTime.UtcNow);

        try
        {
            var response = await next();
            
            _logger.LogInformation("Handled {RequestName} successfully at {DateTime}", 
                requestName, DateTime.UtcNow);
            
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error handling {RequestName} at {DateTime}", 
                requestName, DateTime.UtcNow);
            throw;
        }
    }
}