using System.Data;

namespace CleanArchitecture.Application.Common.Interfaces.Persistence;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}