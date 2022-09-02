using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core;
using Core.Interfaces;
using Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Compiler
{
    internal class Program
    {

        #region IOC


        public static IConfiguration Configuration { get; private set; }
        public static ILogger Logger { get; private set; }
        public static IServiceProvider Container { get; private set; }


        #endregion

        private static void Initialize(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
#if DEBUG
                .AddJsonFile("appsettings.Development.json", true, true)
#endif
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            var services = new ServiceCollection();
            services.AddSingleton(Configuration);
            services.AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Information);
                builder.AddSerilog();
            }).AddOptions();

            // Services

            services.AddCoreServices();

            Container = services.BuildServiceProvider();
            var factory = Container.GetService<ILoggerFactory>();
            if (factory != null)
                Logger = factory.CreateLogger(typeof(Program));

            // Arguments
            _arguments = new Dictionary<string, string>();
            var assembly = Assembly.GetExecutingAssembly().Location;

            if (args == null || !args.Any())
                args = Environment.GetCommandLineArgs();

            foreach (var item in args.Where(m => m != assembly))
            {
                var regex = Regex.Match(item, @"^(?:\/|-)(\w+):?(.+)?$");
                if (regex.Success)
                    _arguments.Add(regex.Groups[1].Value, regex.Groups[2].Value);
            }
        }

        private static Dictionary<string, string> _arguments;

        private static string _game => _arguments.ContainsKey("game") ? _arguments["game"] : string.Empty;
        private static string _map => _arguments.ContainsKey("map") ? _arguments["map"] : string.Empty;

        public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledException;
#if DEBUG
            Console.WriteLine(@"Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
#endif

            Initialize(args);

            var key = ConsoleKey.Enter;
            while (key != ConsoleKey.Escape)
            {
                try
                {
                    MainAsync(args).Start();
                }
                catch (Exception)
                {
                    //ignored
                }

                key = Console.ReadKey().Key;
            }
        }

        private static async Task MainAsync(string[] args)
        {
            await Task.Yield();

            using (var scope = Container.CreateScope())
            using (var context = scope.ServiceProvider.GetRequiredService<ICompileContext>())
            {
                if (context == null) throw new NullReferenceException("Unable to create a compile context");
                context.Compile(_map, preset =>
                {
                    preset.Name = "Temporal";
                    preset.Commands = new[] { "VBSP", "VVIS", "VRAD" };
                    preset.CustomOrder = true;
                });
            }


#if DEBUG
            Console.WriteLine(@"Press any key to finish");
            Console.ReadKey();
#endif
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = (Exception)e.ExceptionObject;
            if (Logger != null)
            {
                Logger.LogError(ex, "{Message}", ex.Message);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
        }
    }
}
