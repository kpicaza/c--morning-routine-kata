// See https://aka.ms/new-console-template for more information


using System.Globalization;
using MorningRoutine;

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

MorningRoutine.MorningRoutine morningRoutine = new MorningRoutine.MorningRoutine(
    new Clock(),
    new ActivityRepository(
        new InMemoryActivityGateway(activities)
    ),
    new ConsoleOutputFormatter()
);

Console.WriteLine(morningRoutine.WhatShouldIDoNow());