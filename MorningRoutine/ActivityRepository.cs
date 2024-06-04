namespace MorningRoutine;

public class ActivityRepository
{
    private readonly IActivityGateway _activityGateway;

    public ActivityRepository(IActivityGateway activityGateway)
    {
        _activityGateway = activityGateway;
    }

    public Activity FindAt(TimeOnly timeOnly)
    {
        List<Activity> activities = _activityGateway.All();

        foreach (var activity in activities)
        {
            if (activity.IsAvailableAt(timeOnly))
            {
                return activity;
            }
        }

        return Activity.NoActivity();
    }
}
