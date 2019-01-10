using Microsoft.Extensions.DependencyInjection;
using WebApp;

namespace CompositionRoot
{
    public class NonUiDependencyRegistry : INonUiDependencyRegistry
    {
        public IServiceCollection RegisterAllNonUiDependencies(IServiceCollection services)
        {
            return services.AddDatalayer();
        }
    }
}