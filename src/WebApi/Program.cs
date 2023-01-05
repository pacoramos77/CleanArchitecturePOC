using Hellang.Middleware.ProblemDetails;

using SharedKernel.Domain.Exceptions;

using WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCoreServices().AddInfrastructureServices();
builder.Services.AddTransient<ExceptionHandlingMiddleware>();
builder.Services.AddProblemDetails(opts =>
{
    opts.IncludeExceptionDetails = (context, _) =>
    {
        var environment = context.RequestServices.GetRequiredService<IHostEnvironment>();
        return environment.IsDevelopment();
    };
    opts.Map<Exception>(ex => ApplicationProblemDetails.FromException(ex));
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    builder.Services.DebugServices();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseProblemDetails();

// app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
