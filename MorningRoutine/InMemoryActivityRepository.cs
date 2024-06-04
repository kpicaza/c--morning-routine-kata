namespace MorningRoutine;

public class InMemoryActivityGateway: IActivityGateway
{
    private List<Activity> _activities;

    public InMemoryActivityGateway(List<Activity> activities)
    {
        _activities = activities;
    }

    public List<Activity> All()
    {
        return _activities;
    }
}
