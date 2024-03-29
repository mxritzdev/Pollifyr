using Pollifyr.App.Helpers;
using Pollifyr.App.Helpers.Utils;

namespace Pollifyr.App.Services.Partials;

public class DateTimeService
{
    public long GetCurrentUnix()
    {
        return new DateTimeOffset(GetCurrent()).ToUnixTimeMilliseconds();
    }
    
    public long GetCurrentUnixSeconds()
    {
        return new DateTimeOffset(GetCurrent()).ToUnixTimeSeconds();
    }

    public DateTime GetCurrent()
    {
        return DateTime.UtcNow;
    }

    public string GetDate()
    {
        return Formatter.FormatDateOnly(GetCurrent());
    }

    public string GetDateTime()
    {
        return Formatter.FormatDate(GetCurrent());
    }
}