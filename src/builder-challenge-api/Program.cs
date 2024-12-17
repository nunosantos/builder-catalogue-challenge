using builder_challenge_application.Interfaces;
using builder_challenge_application.Services;
using builder_challenge_domain.Interfaces;
using builder_challenge_infrastructure;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddJsonFile("appsettings.json", false, true)
        .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true)
        .AddEnvironmentVariables();

    builder.Services.AddControllers();

    builder.Services.AddScoped<IBuildableSetService, BuildableSetService>();

    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddScoped<IColourService, ColourService>();

    builder.Services.AddScoped<ISetService, SetService>();

    var baseUrl = builder.Configuration["ApiSettings:BaseUrl"];

    if (string.IsNullOrEmpty(baseUrl))
        throw new InvalidOperationException(
            "API base URL is not configured. Please set 'ApiSettings:BaseUrl' in appsettings.json.");

    // Register ColourRepository with HttpClient
    builder.Services.AddHttpClient<IColourRepository, ColourRepository>(client =>
    {
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

// Register UserRepository with HttpClient
    builder.Services.AddHttpClient<IUserRepository, UserRepository>(client =>
    {
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

// Register SetRepository with HttpClient
    builder.Services.AddHttpClient<ISetRepository, SetRepository>(client =>
    {
        client.BaseAddress = new Uri(baseUrl);
        client.DefaultRequestHeaders.Add("Accept", "application/json");
    });

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.UseSwagger();

    app.UseSwaggerUI();

    app.UseRouting();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception ex)
{
    //do something with the exception like logging
}

public partial class Program
{
}