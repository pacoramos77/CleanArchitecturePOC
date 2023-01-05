using Microsoft.AspNetCore.Mvc;

namespace SharedKernel.Exceptions;

public sealed class ApplicationProblemDetails : ProblemDetails
{
    required public IReadOnlyDictionary<string, string[]>? Errors { get; init; }
}
