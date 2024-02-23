using CleanArchitecture.Application.Common.Interfaces.Services;

namespace CleanArchitecture.Infrastructure.Services;

public class DateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}
