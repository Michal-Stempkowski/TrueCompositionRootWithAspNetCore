using CoreApplication;
using Datalayer;

// ReSharper disable once CheckNamespace - Microsoft recommended practice
namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatalayer(this IServiceCollection services)
        {
            return services.AddTransient<IRepository, ConcreteRepository>();
        }
    }
}