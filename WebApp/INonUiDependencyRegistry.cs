using Microsoft.Extensions.DependencyInjection;

namespace WebApp
{
    public interface INonUiDependencyRegistry
    {
        IServiceCollection RegisterAllNonUiDependencies(IServiceCollection services);
    }
}