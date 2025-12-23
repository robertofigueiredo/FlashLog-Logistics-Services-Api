using FlashLog.LogisticsService.Api;
using FlashLog.LogisticsService.Api.Logging;
using FlashLog.LogisticsService.Api.Middleware;
using FlashLog.LogisticsService.Api.Routes;
using FlashLog.LogisticsService.Application;
using FlashLog.LogisticsService.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(LoggerConfig.SerilogConfig);

//Api
builder.Services.AddApiDependencyInjection();

//Application
builder.Services.AddApplicationLayer();

//Infrastructure
builder.Services.AddInfrastructureLayer();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseExceptionHandler(error =>
{
    error.Run(GlobalExceptionHandler.ConfigureExceptionHandler);
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapOrderRoutes();
app.Run();