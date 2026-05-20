using Application;
using Infrastructure;
using Serilog;
using Web.Api;
using Web.Api.Extensions;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console().CreateLogger();

builder.Host.UseSerilog();
builder.Services.AddVersioning();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddPresentation();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapVersionedEndpoints();
app.UseSwaggerWithUi();
app.ApplyMigrations();
app.Run();
