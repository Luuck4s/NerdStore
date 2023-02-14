using NerdStore.Core.Extensions;

namespace NerdStore.Vendas.Api.Configuration;

public static class JsonServiceCollectionExtensions
{
    public static void AddJsonConverter(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(
                options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
                });
    }
}