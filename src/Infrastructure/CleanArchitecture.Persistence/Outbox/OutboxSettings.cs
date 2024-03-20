namespace CleanArchitecture.Persistence.Outbox;

public sealed class OutboxSettings
{
    public int IntervalInSeconds { get; init; }

    public int BatchSize { get; init; }
}
