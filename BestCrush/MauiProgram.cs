using System.Text;
using BestCrush.Domain;
using BestCrush.Domain.Services;
using BestCrush.Domain.Services.Upgrades;
using DofusSharp.DofusDb.ApiClients;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace BestCrush;

public static class MauiProgram
{
    const string DatabaseFileName = "bestcrush.db";
    static readonly string DbPath = Path.Combine(FileSystem.AppDataDirectory, DatabaseFileName);
    static readonly string LogPath = Path.Combine(FileSystem.AppDataDirectory, "bestcrush.log");

    public static MauiApp CreateMauiApp()
    {
        SetupSerilog();

        try
        {
            SerilogLoggerFactory loggerFactory = new(Log.Logger);
            ILogger logger = loggerFactory.CreateLogger("Bootstrap");

            logger.LogInformation("Application starting...");

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); })
                .ConfigureEssentials(essentials => essentials.UseVersionTracking());

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            ConfigureDatabase(builder, logger);

            builder.Services.AddSingleton(new ImageCache(Path.Combine(FileSystem.CacheDirectory, "images")));
            builder.Services.AddSingleton<ServersService>();
            builder.Services.AddSingleton<RunesService>();
            builder.Services.AddSingleton<CharacteristicsService>();
            builder.Services.AddSingleton<CrushService>();
            builder.Services.AddScoped<ApplicationUpgradesHandler>();
            builder.Services.AddScoped<DofusDbUpgradesHandler>();
            builder.Services.AddScoped<ItemsService>();

#if DEBUG
            builder.Services.AddSingleton(DofusDbQuery.Beta(new Uri("http://localhost/BestCrush")));
            builder.Services.AddSingleton(DofusDbClient.Beta(new Uri("http://localhost/BestCrush")));
#else
            builder.Services.AddSingleton(DofusDbQuery.Production(new Uri("http://localhost/BestCrush")));
            builder.Services.AddSingleton(DofusDbClient.Production(new Uri("http://localhost/BestCrush")));
#endif

            builder.Logging.ClearProviders();
            builder.Logging.AddSerilog(Log.Logger, true);

            MauiApp app = builder.Build();

            logger.LogInformation("Application started.");

            return app;
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Application start-up failed.");
            throw;
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    static LoggerConfiguration SetupSerilog()
    {
        TimeSpan flushInterval = new(0, 0, 1);

        LoggerConfiguration configuration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(LogPath, flushToDiskInterval: flushInterval, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100_000_000);

#if DEBUG
        configuration.WriteTo.Debug();
#endif

        Log.Logger = configuration.CreateLogger();

        return configuration;
    }

    static void ConfigureDatabase(MauiAppBuilder builder, ILogger logger)
    {
        if (!Directory.Exists(FileSystem.AppDataDirectory))
        {
            Directory.CreateDirectory(FileSystem.AppDataDirectory);
        }

        builder.Services.AddDbContext<BestCrushDbContext>(options => options.UseSqlite($"Data Source={DbPath}"));

        logger.LogInformation("Databases configured at {DbPath}.", DbPath);
    }
}
