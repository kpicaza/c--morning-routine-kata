namespace MorningRoutine;

public class MorningRoutine
{
    private readonly Clock _clock;
    private readonly ActivityRepository _activityRepository;
    private readonly ConsoleOutputFormatter _outputFormatter;

    public MorningRoutine(
        Clock clock,
        ActivityRepository activityRepository,
        ConsoleOutputFormatter outputFormatter
    ) {
        _clock = clock;
        _activityRepository = activityRepository;
        _outputFormatter = outputFormatter;
    }

    public String WhatShouldIDoNow()
    {
        var activity = _activityRepository.FindAt(_clock.CurrentTime());

        return _outputFormatter.Print(activity);
    }
}