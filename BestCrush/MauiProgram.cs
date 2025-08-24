using System.Text;
using BestCrush.Persistence;
using BestCrush.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;
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
        LoggerConfiguration loggerConfiguration = SetupSerilog();

        try
        {
            SerilogLoggerFactory loggerFactory = new(Log.Logger);
            ILogger logger = loggerFactory.CreateLogger("Bootstrap");

            logger.LogInformation("Application starting...");

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

            builder.Logging.AddSerilog();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            ConfigureDatabase(builder, logger);

            builder.Services.AddSingleton<ImageCache>();
            builder.Services.AddSingleton<ServersService>();
            builder.Services.AddSingleton<RunesService>();
            builder.Services.AddSingleton<ItemsService>();

            MauiApp app = builder.Build();

            MigrateDatabaseAsync(app, logger).GetAwaiter().GetResult();

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

        LoggerConfiguration configuration = new LoggerConfiguration().MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.File(LogPath, flushToDiskInterval: flushInterval, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100_000_000);
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

        logger.LogInformation("Database configured at {DbPath}.", DbPath);
    }

    static async Task MigrateDatabaseAsync(MauiApp app, ILogger logger)
    {
        logger.LogInformation("Migrating database...");

        await using BestCrushDbContext context = app.Services.GetRequiredService<BestCrushDbContext>();
        await context.Database.MigrateAsync();

        logger.LogInformation("Done migrating database.");
    }
}
