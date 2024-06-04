using Moq;

namespace MorningRoutine.Test.Unit;

public class ActivityRepositoryTest
{
    [Test]
    public void Should_return_an_activity_for_given_Time()
    {
        List<Activity> activities = new List<Activity>();
        activities.Add(new Activity(
            "Leer",
            new TimeOnly(7, 00),
            new TimeOnly(7, 29)
        ));
        
        Mock<IActivityGateway> activityGatewayMock = new Mock<IActivityGateway>();
        activityGatewayMock.Setup(gateway => gateway.All())
            .Returns(activities);
        
        ActivityRepository activityRepository = new ActivityRepository(activityGatewayMock.Object);

        Activity activity = activityRepository.FindAt(new TimeOnly(7, 00));
        
        Assert.That(activity.Name, Is.SameAs("Leer"));
        Assert.That(activity.StartsAt, Is.EqualTo(new TimeOnly(7, 00)));
        Assert.That(activity.EndAt, Is.EqualTo(new TimeOnly(7, 29)));
    }
}