using Hellang.Middleware.ProblemDetails;

using SharedKernel.Exceptions;

using ApplicationException = SharedKernel.Exceptions.ApplicationException;
using ValidationException = SharedKernel.Exceptions.ValidationException;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionProblemDetailsExtension
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(opts =>
        {
            opts.IncludeExceptionDetails = (context, ex) =>
            {
                var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
                return environment.IsDevelopment() && ex is not ApplicationException;
            };
            opts.Map<Exception>((ctx, ex) => ex.ToProblemDetails(ctx));
        });

        return services;
    }

    public static ApplicationProblemDetails ToProblemDetails(this Exception ex, HttpContext? ctx)
    {
        var statusCode = GetStatusCode(ex);
        return new()
        {
            Type = $"https://httpstatuses.io/{statusCode}",
            Detail = GetExceptionMessage(ex),
            Title = GetTitle(ex),
            Errors = GetErrors(ex),
            Status = statusCode,
            Instance = ctx?.Request.Path,
        };
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadRequestException => StatusCodes.Status400BadRequest,
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            DomainException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    private static string GetTitle(Exception exception) =>
        exception switch
        {
            ApplicationException applicationException => applicationException.Title,
            _ => "Server Error"
        };

    private static IReadOnlyDictionary<string, string[]>? GetErrors(Exception exception) =>
        exception switch
        {
            ValidationException validationException => validationException.ErrorsDictionary,
            _ => null
        };

    private static string GetExceptionMessage(Exception ex)
    {
        if (ex.InnerException is not null)
        {
            return GetExceptionMessage(ex.InnerException);
        }

        return ex.Message;
    }
}
