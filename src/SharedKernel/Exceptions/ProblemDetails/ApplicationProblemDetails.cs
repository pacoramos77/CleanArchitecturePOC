using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SharedKernel.Exceptions;

using ApplicationException = SharedKernel.Exceptions.ApplicationException;

namespace SharedKernel.Domain.Exceptions
{
    public sealed class ApplicationProblemDetails : ProblemDetails
    {
        public IReadOnlyDictionary<string, string[]>? Errors { get; private set; }

        public static ApplicationProblemDetails FromException(Exception ex)
        {
            return new()
            {
                Title = GetTitle(ex),
                Detail = ex.Message,
                Status = GetStatusCode(ex),
                Errors = GetErrors(ex)
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
    }
}
