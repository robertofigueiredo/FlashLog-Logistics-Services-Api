using System.Diagnostics.CodeAnalysis;

namespace FlashLog.LogisticsService.Api.Configuration;

public class LogConfig
{
    public string? GelfUrl { get; set; }
    public int? GelfPort { get; set; }

    [MemberNotNullWhen(true, nameof(GelfUrl), nameof(GelfPort))]
    public bool IsGelfActive => !string.IsNullOrEmpty(GelfUrl) && GelfPort is not null;
}
