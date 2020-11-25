using System.Linq;
using System.Reflection;
using WebWindows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Looper.Core;

namespace Looper
{
    public static class ServicesConfiguration
    {
        public static void AddControllers(this IServiceCollection services)
        {
            var types = Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsSubclassOf(typeof(Controller)));
            foreach (var type in types)
            {
                if (type.IsInterface || type.IsAbstract) continue;
                services.AddSingleton(typeof(IController), type);
            }
        }

        public static void AddBehaviours(this IServiceCollection services)
        {
            var types = Assembly.GetEntryAssembly().GetTypes().Where(t => typeof(IBehaviour).IsAssignableFrom(t));
            foreach (var type in types)
            {
                if (type.IsInterface || type.IsAbstract) continue;
                services.AddSingleton(typeof(IBehaviour), type);
            }
        }

        public static void AddCommands(this IServiceCollection services)
        {
            var types = Assembly.GetEntryAssembly().GetTypes().Where(t => typeof(ICommand).IsAssignableFrom(t));
            foreach (var type in types)
            {
                if (type.IsInterface) continue;
                services.AddSingleton(typeof(ICommand), type);
            }
        }
    }

    public class AppBuilder
    {
        public ServiceCollection services = new ServiceCollection();
        private ServiceProvider provider;

        public AppBuilder Configure()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<WebWindow>((sp) => new WebWindow(""));
            services.AddSingleton<App>();
            services.AddSingleton<Router>();
            services.AddCommands();
            services.AddBehaviours();
            services.AddControllers();
            return this;
        }

        public App Build()
        {
            provider = services.BuildServiceProvider();
            var controllers = provider.GetServices<IController>();
            foreach (var controller in controllers)
            {
                controller.RegisterRoutes();
            }
            return provider.GetService<App>();
        }
    }
}