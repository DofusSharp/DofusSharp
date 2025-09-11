using System.Text;
using BestCrush.Domain;
using BestCrush.Domain.Services;
using BestCrush.Domain.Services.Upgrades;
using DofusSharp.Dofocus.ApiClients;
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

    public static MauiApp CreateMauiApp()
    {
        string dataDirectory = GetDataDirectory();
        // By default the web view data folder is next to the executable, which might be in a protected location like Program Files.
        Environment.SetEnvironmentVariable("WEBVIEW2_USER_DATA_FOLDER", dataDirectory);

        string dbPath = Path.Combine(dataDirectory, "Data", DatabaseFileName);
        string logPath = Path.Combine(dataDirectory, "Logs", "bestcrush.log");

        SetupSerilog(logPath);

        try
        {
            SerilogLoggerFactory loggerFactory = new(Log.Logger);
            ILogger logger = loggerFactory.CreateLogger("Bootstrap");

            logger.LogInformation("Application starting...");
            logger.LogInformation("Version of binaries: {Version}.", CurrentVersion.Version);

            MauiAppBuilder builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts => { fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular"); });

            builder.Services.AddMauiBlazorWebView();
#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
#endif

            ConfigureDatabase(builder, dbPath, logger);

            builder.Services.AddSingleton(new ImageCache(Path.Combine(dataDirectory, "Cache", "images")));
            builder.Services.AddSingleton<ServersService>();
            builder.Services.AddSingleton<RunesService>();
            builder.Services.AddSingleton<CharacteristicsService>();
            builder.Services.AddSingleton<CrushService>();
            builder.Services.AddScoped<ApplicationUpgradesHandler>();
            builder.Services.AddScoped<GameDataUpgradeHandler>();
            builder.Services.AddScoped<ItemsService>();

#if DEBUG
            builder.Services.AddSingleton(DofusDbQuery.Beta(new Uri("https://github.com/DofusSharp/DofusSharp/tree/main/BestCrush")));
            builder.Services.AddSingleton(DofusDbClient.Beta(new Uri("https://github.com/DofusSharp/DofusSharp/tree/main/BestCrush")));
#else
            builder.Services.AddSingleton(DofusDbQuery.Production(new Uri("https://github.com/DofusSharp/DofusSharp/tree/main/BestCrush")));
            builder.Services.AddSingleton(DofusDbClient.Production(new Uri("https://github.com/DofusSharp/DofusSharp/tree/main/BestCrush")));
#endif

            builder.Services.AddSingleton(DofocusClient.Production());

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

    static LoggerConfiguration SetupSerilog(string logPath)
    {
        TimeSpan flushInterval = new(0, 0, 1);

        LoggerConfiguration configuration = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(logPath, flushToDiskInterval: flushInterval, encoding: Encoding.UTF8, rollingInterval: RollingInterval.Day, fileSizeLimitBytes: 100_000_000);

#if DEBUG
        configuration.WriteTo.Debug();
#endif

        Log.Logger = configuration.CreateLogger();

        return configuration;
    }

    static void ConfigureDatabase(MauiAppBuilder builder, string dbPath, ILogger logger)
    {
        string? directory = Path.GetDirectoryName(dbPath);
        if (directory is not null && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        builder.Services.AddDbContext<BestCrushDbContext>(options => options.UseSqlite($"Data Source={dbPath}"));

        logger.LogInformation("Databases configured at {DbPath}.", dbPath);
    }

    static string GetDataDirectory()
    {
#if WINDOWS
        // use %LOCALAPPDATA% on Windows because it is the path that is cleaned by the installer on uninstall
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "BestCrush");
#else
        return FileSystem.AppDataDirectory;
#endif
    }
}
