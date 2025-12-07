using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace DDD.Application.Common.Behaviors;

public class PerformanceBehavior<TRequest, TResponse>
{
    private readonly ILogger<PerformanceBehavior<TRequest, TResponse>> _logger;
    private readonly Stopwatch _timer;

    public PerformanceBehavior(ILogger<PerformanceBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
        _timer = new Stopwatch();
    }

    public async Task<TResponse> Handle(
        TRequest request,
        Func<Task<TResponse>> next,
        CancellationToken cancellationToken)
    {
        _timer.Start();
        
        var response = await next();
        
        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 500)
        {
            var requestName = typeof(TRequest).Name;
            
            _logger.LogWarning(
                "Long Running Request: {RequestName} ({ElapsedMilliseconds} ms)",
                requestName, elapsedMilliseconds);
        }

        return response;
    }
}
