using System.Text.Json.Serialization;
using NerdStore.Core.Extensions;

namespace NerdStore.Api.Configuration;

public static class JsonServiceCollectionExtensions
{
    public static void AddJsonConverter(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                });
    }
}