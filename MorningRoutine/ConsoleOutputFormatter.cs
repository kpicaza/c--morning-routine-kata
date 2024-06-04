using System.Globalization;

namespace MorningRoutine;

public class ConsoleOutputFormatter
{
    public string Print(Activity activity)
    {
        if (null == activity.StartsAt)
        {
            return activity.Name;
        }
        
        return string.Format(
            "De {0} a {1} - {2}",
            activity.StartsAt?.ToString("t", CultureInfo.InvariantCulture),
            activity.EndAt?.ToString("t", CultureInfo.InvariantCulture),
            activity.Name
        );
    }
}
