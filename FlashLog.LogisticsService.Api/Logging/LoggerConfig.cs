using FlashLog.LogisticsService.Api.Configuration;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace FlashLog.LogisticsService.Api.Logging;

public class LoggerConfig
{
    public static readonly Action<HostBuilderContext, LoggerConfiguration> SerilogConfig = (context, config) =>
    {
        const string outputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] [{ClientIp}] {Message} {Scope} {Exception}{NewLine}";
        const string applicationName = "Liderum.Security";

        var logConfig = context.Configuration
            .GetSection(nameof(LogConfig))
            .Get<LogConfig>();

        config
            .WriteTo.File(
                $"C:/Logs/{applicationName}/log.txt",
                rollingInterval: RollingInterval.Day,
                restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error,
                outputTemplate: outputTemplate)
            .WriteTo.Console(
                outputTemplate: outputTemplate)
            .Enrich.WithClientIp()
            .Enrich.WithProperty("ApplicationName", applicationName)
            .Enrich.FromLogContext();

        if (logConfig?.IsGelfActive ?? false)
        {
            config.WriteTo.Graylog(logConfig.GelfUrl, logConfig.GelfPort.Value, TransportType.Tcp);
        }

        config.Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.Hosting"));
    };
}