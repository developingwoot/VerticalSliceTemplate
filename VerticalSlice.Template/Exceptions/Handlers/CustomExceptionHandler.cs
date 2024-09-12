using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, $"Error: {exception.GetType().Name} {exception.Message}", cancellationToken);
        
        var statusCode = exception switch {
            ValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        var problem = new ProblemDetails {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = statusCode,
            Instance = httpContext.Request.Path
        };

        if(exception is ValidationException validationException){
            problem.Extensions.Add("ValidationExceptions", validationException.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);

        return true;
    }
}