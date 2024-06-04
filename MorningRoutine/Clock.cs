namespace MorningRoutine;

public class Clock
{
    public virtual TimeOnly CurrentTime()
    {
        return TimeOnly.FromDateTime(DateTime.Now);
    }
}