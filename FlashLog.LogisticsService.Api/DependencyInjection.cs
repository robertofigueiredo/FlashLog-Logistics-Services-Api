using FlashLog.LogisticsService.Api.Configuration;

namespace FlashLog.LogisticsService.Api;

public static class DependencyInjection
{
    public static void AddApiDependencyInjection(this IServiceCollection services)
    {

        services.AddOptions<LogConfig>()
                .BindConfiguration(nameof(LogConfig));
    }
}