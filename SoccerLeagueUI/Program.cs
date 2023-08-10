using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using SoccerLeagueDomain;
using SoccerLeagueClassLibrary.BusinessLogic;
using SoccerLeagueClassLibrary.Repository;
using SoccerLeagueUI.InputHandlers;
using SoccerLeagueUI.Output;
using SoccerLeagueUI.Services;


namespace SoccerLeagueUI
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var configuration = BuildConfiguration();
            ConfigureLogging(configuration);

            Log.Information("Application Starting");

            using var host = CreateHostBuilder(args, configuration).Build();
            host.Services.GetRequiredService<ISoccerLeagueService>().Run();
        }

        private static IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static void ConfigureLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        private static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    ConfigureServices(services, configuration);
                })
                .UseSerilog();

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IManualInputHandler, ManualInputHandler>();
            services.AddScoped<IFileInputHandler, FileInputHandler>();


            services.AddDbContext<SoccerLeagueDBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("SoccerLeagueConnection") + ";TrustServerCertificate=true"));

            services.AddScoped<IInputValidatorService, InputValidatorService>();
            services.AddScoped<ISoccerLeagueRepository, SoccerLeagueRepository>();
            services.AddScoped<ISoccerLeagueService, SoccerLeagueService>();
            services.AddScoped<ISoccerMatchProcessorService, SoccerMatchProcessorService>();
            services.AddSingleton<ISoccerLeague, SoccerLeague>();
            services.AddScoped<ITeamScorePrinter, TeamScorePrinter>();
        }
    }
}
