using System.Reflection;
using System.Text.Json;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LoyaltyNet.Web.Infrastructure.Configurations;

public sealed class MvcConfigurator : IApplicationConfigurator
{
    public int Order => 0;

    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            options.ModelMetadataDetailsProviders.Add(new SystemTextJsonValidationMetadataProvider(JsonNamingPolicy.CamelCase));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            options.AllowInputFormatterExceptionMessages = false;
        });

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory;
        });

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddHostedService<StartupHostedService>();
    }

    public void ConfigureMiddleware(IApplicationBuilder builder) { }

    private static ContentResult InvalidModelStateResponseFactory(ActionContext context)
    {
        var problem = ProblemResponse.Of(context.ModelState);

        return new ContentResult
        {
            StatusCode = problem.Status,
            Content = problem.ToJson(),
            ContentType = "application/json"
        };
    }
}
