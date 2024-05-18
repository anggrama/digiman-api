using Asp.Versioning;
using digiman_api.Extensions;
using Serilog;

try
{
    Log.Information("Starting digidocu api");

    var builder = WebApplication.CreateBuilder(args);

    // Add logging
    builder.Host.UseSerilog((context, configuration) => 
        configuration.ReadFrom.Configuration(context.Configuration));

    // Add services to the container.
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddControllers();
    builder.Services.AddBusinessServices();
    builder.Services.AddSerilog();
    builder.Services.AddVersioning();
    builder.Services.AddSwaggerGen();
    builder.Services.AddDatabaseContext();

    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    app.UseAuthorization();
    app.UseSerilogRequestLogging();
    app.MapControllers();
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
