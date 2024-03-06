using System.Data;

namespace CleanArchitecture.Application.Common.Persistence;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
