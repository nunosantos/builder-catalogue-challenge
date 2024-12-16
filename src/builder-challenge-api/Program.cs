var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// builder.Host.UseSerilog((context, _, loggerConfiguration) =>
// {
//     loggerConfiguration
//         .ReadFrom.Configuration(context.Configuration)
//         .Enrich.WithProperty("Application", "order-svc")
//         .Enrich.WithProperty("Environment", "Dev")
//         .WriteTo.Console(new RenderedCompactJsonFormatter())
//         .WriteTo.GrafanaLoki("http://loki:3100");
// });

// builder.Services.UseHttpClientMetrics();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

app.UseRouting();

app.MapControllers();

await app.RunAsync();

public partial class Program {}