using System.Globalization;

namespace MorningRoutine.Test.Feature;

/**
 *
 * De 06:00 a 06:59 - Hacer ejercicio
 * De 07:00 a 07:29 - Leer
 * De 07:30 a 07:59 - Estudiar
 * De 08:00 a 08:59 - Desayunar
 *
 * De 06:00 a 06:44 - Hacer ejercicio
 * De 06:45 a 06:59 - Ducharse
 * De 07:00 a 07:29 - Leer
 * De 07:30 a 07:59 - Estudiar
 * De 08:00 a 09:00 - Desayunar
 * 
 */
public class MorningRoutineTest
{
    private ConsoleOutputFormatter _outputFormatter;
    private TestClock _clock;

    [SetUp]
    public void Setup()
    {
        _outputFormatter = new ConsoleOutputFormatter();
        _clock = new TestClock();
    }
    
    [Test]
    public void Should_show_Activities_At_Current_Times()
    {
        List<Activity> activities = new List<Activity>();
        activities.Add(new Activity(
            "Hacer ejercicio",
            TimeOnly.Parse("06:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("06:59", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Leer",
            TimeOnly.Parse("07:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("07:29", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Estudiar",
            TimeOnly.Parse("07:30", CultureInfo.InvariantCulture),
            TimeOnly.Parse("07:59", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Desayunar",
            TimeOnly.Parse("08:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("08:59", CultureInfo.InvariantCulture)
        ));

        IActivityGateway activityGateway = new InMemoryActivityGateway(activities);
        

        MorningRoutine morningRoutine = new MorningRoutine(
            _clock,
            new ActivityRepository(
                activityGateway
            ),
            _outputFormatter
        );
        
        _clock.SetTime(new TimeOnly(6, 59));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 06:00 a 06:59 - Hacer ejercicio"));
        _clock.SetTime(new TimeOnly(7, 29));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 07:00 a 07:29 - Leer"));
        _clock.SetTime(new TimeOnly(7, 59));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 07:30 a 07:59 - Estudiar"));
        _clock.SetTime(new TimeOnly(8, 59));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 08:00 a 08:59 - Desayunar"));
    }

    [Test]
    public void Should_show_Another_Activities_At_Current_Times()
    {
        List<Activity> activities = new List<Activity>();
        activities.Add(new Activity(
            "Hacer ejercicio",
            TimeOnly.Parse("06:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("06:44", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Ducharse",
            TimeOnly.Parse("06:45", CultureInfo.InvariantCulture),
            TimeOnly.Parse("06:59", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Leer",
            TimeOnly.Parse("07:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("07:29", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Estudiar",
            TimeOnly.Parse("07:30", CultureInfo.InvariantCulture),
            TimeOnly.Parse("07:59", CultureInfo.InvariantCulture)
        ));
        activities.Add(new Activity(
            "Desayunar",
            TimeOnly.Parse("08:00", CultureInfo.InvariantCulture),
            TimeOnly.Parse("08:59", CultureInfo.InvariantCulture)
        ));

        IActivityGateway activityGateway = new InMemoryActivityGateway(activities);
        

        MorningRoutine morningRoutine = new MorningRoutine(
            _clock,
            new ActivityRepository(
                activityGateway
            ),
            _outputFormatter
        );
        
        _clock.SetTime(new TimeOnly(6, 44));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 06:00 a 06:44 - Hacer ejercicio"));
        _clock.SetTime(new TimeOnly(6, 59));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 06:45 a 06:59 - Ducharse"));
        _clock.SetTime(new TimeOnly(7, 29));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 07:00 a 07:29 - Leer"));
        _clock.SetTime(new TimeOnly(7, 59));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 07:30 a 07:59 - Estudiar"));
        _clock.SetTime(new TimeOnly(8, 59));
        Assert.That(morningRoutine.WhatShouldIDoNow(), Is.EqualTo("De 08:00 a 08:59 - Desayunar"));
    }
}

public class TestClock : Clock
{
    private TimeOnly _timeOnly;

    public void SetTime(TimeOnly timeOnly)
    {
        _timeOnly = timeOnly;
    }

    public override TimeOnly CurrentTime()
    {
        return _timeOnly;
    }
}