using uRADMonitorDataReceiver;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.secret.json", optional: true, reloadOnChange: false);

builder.Logging.ClearProviders().AddConsole();
builder.Logging.SetMinimumLevel(LogLevel.Trace);

Startup.ConfigureServices(builder);

var app = builder.Build();

Startup.ConfigurePipeline(app);

app.Run();