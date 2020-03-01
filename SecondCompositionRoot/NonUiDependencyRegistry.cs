using Microsoft.Extensions.DependencyInjection;
using WebApp;

namespace SecondCompositionRoot
{
    public class NonUiDependencyRegistry : INonUiDependencyRegistry
    {
        public IServiceCollection RegisterAllNonUiDependencies(IServiceCollection services)
        {
            return services.AddDatalayer();
        }
    }
}