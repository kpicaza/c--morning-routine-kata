using System.Globalization;

namespace MorningRoutine.Test.Unit;


public class MorningRoutineTest
{
    private InMemoryActivityGateway _activityGateway;
    private ConsoleOutputFormatter _outputFormatter;

    [SetUp]
    public void Setup()
    {
        List<Activity> activities = new List<Activity>();
        activities.Add(new Activity(
            "Hacer ejercicio",
            TimeOnly.Parse("06:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("06:59", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Leer y estudiar",
            TimeOnly.Parse("07:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("07:59", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Desayunar",
            TimeOnly.Parse("08:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("08:59", CultureInfo.InvariantCulture)
        ));
        
        _activityGateway = new InMemoryActivityGateway(activities);
        _outputFormatter = new ConsoleOutputFormatter();
    }
    
    [Test]
    public void Should_show_Make_Exercise_between_6_00_and_6_59()
    {
        Clock clock = new TestClock(new TimeOnly(6, 00));
        
        MorningRoutine morningRoutine = new MorningRoutine(
            clock,
            new ActivityRepository(
                _activityGateway
            ),
        _outputFormatter
        );
        
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 06:00 a 06:59 - Hacer ejercicio"));
    }

    [Test]
    public void Should_show_Read_and_Study_between_7_00_and_7_59()
    {
        Clock clock = new TestClock(new TimeOnly(7, 00));

        MorningRoutine morningRoutine = new MorningRoutine(
            clock,
            new ActivityRepository(
                _activityGateway
            ),
            _outputFormatter
            );
        
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 07:00 a 07:59 - Leer y estudiar"));
    }

    [Test]
    public void Should_show_Breakfast_between_8_00_and_8_59()
    {
        Clock clock = new TestClock(new TimeOnly(8, 00));

        MorningRoutine morningRoutine = new MorningRoutine(
            clock,
            new ActivityRepository(
                _activityGateway
            ),
            _outputFormatter
        );
        
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 08:00 a 08:59 - Desayunar"));
    }

    [Test]
    public void Should_show_No_Activity_out_of_morning_calendar()
    {
        Clock clock = new TestClock(new TimeOnly(5, 59));

        MorningRoutine morningRoutine = new MorningRoutine(
            clock,
            new ActivityRepository(
                _activityGateway
            ),
            _outputFormatter
        );
        
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("Sin actividad"));
    }
}

public class TestClock : Clock
{
    private readonly TimeOnly _timeOnly;

    public TestClock(TimeOnly timeOnly)
    {
        _timeOnly = timeOnly;
    }

    public override TimeOnly CurrentTime()
    {
        return _timeOnly;
    }
}