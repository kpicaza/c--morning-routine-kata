namespace MorningRoutine;

public class Activity
{
    public readonly string Name;
    public readonly TimeOnly? StartsAt;
    public readonly TimeOnly? EndAt;

    public Activity(
        string name,
        TimeOnly? startsAt,
        TimeOnly? endAt
    )
    {
        Name = name;
        StartsAt = startsAt;
        EndAt = endAt;
    }

    public static Activity NoActivity()
    {
        return new Activity("Sin actividad", null, null);
    }

    public bool IsAvailableAt(TimeOnly timeOnly)
    {
        return timeOnly >= StartsAt && timeOnly <= EndAt;
    }
}
