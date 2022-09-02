using Core.Interfaces;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddTransient<IParameterManager, ParameterManager>();
            services.AddTransient<IPresetManager, PresetManager>();
            services.AddTransient<ISettingManager, SettingManager>();

            services.AddScoped<ICompileContext, CompileContext>();

            return services;
        }
    }
}
