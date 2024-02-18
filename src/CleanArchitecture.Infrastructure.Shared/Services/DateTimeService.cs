using CleanArchitecture.Application.Common.Interfaces.Services;

namespace CleanArchitecture.Infrastructure.Shared.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}
