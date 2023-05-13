using Microsoft.AspNetCore.Diagnostics;
using Smakoowa_Api;
using Smakoowa_Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

ServicesConfigurator.ConfigureIdentity(builder);
ServicesConfigurator.ConfigureSwagger(builder);
ServicesConfigurator.ConfigureDatabaseConnection(builder);
ServicesConfigurator.ConfigureCors(builder);
ServicesConfigurator.ConfigureBackgroundQueue(builder);
ServicesConfigurator.ConfigureServices(builder.Services);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(sp => new HttpClient());
builder.Services.AddHttpContextAccessor();

ServicesConfigurator.ConfigureDatabaseConnection(builder);

ServicesConfigurator.ConfigureServices(builder.Services);

builder.Services.AddSingleton<IBackgroundTaskQueue>(_ =>
{
    if (!int.TryParse(builder.Configuration["QueueCapacity"], out var queueCapacity))
    {
        queueCapacity = 1000;
    }
    return new DefaultBackgroundTaskQueue(queueCapacity);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("corspolicy",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173", "https://localhost:5173", "https://smakoowa-web-app.vercel.app")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
        });
});

configuration = builder.Configuration;

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseExceptionHandler(c => c.Run(async context =>
{
    var exception = context.Features.Get<IExceptionHandlerPathFeature>().Error;

    if (exception is SecurityTokenValidationException)
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
    else
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }

    await context.Response.WriteAsJsonAsync(ServiceResponse.Error(exception.Message, (HttpStatusCode)context.Response.StatusCode));
}));

app.UseCors("corspolicy");
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.UseStaticFiles();

app.UseMiddleware<ServiceResponseMiddleware>();
app.UseMiddleware<RequestCountMiddleware>();

app.Run();

public partial class Program
{
    public static IConfiguration configuration { get; private set; }
}
