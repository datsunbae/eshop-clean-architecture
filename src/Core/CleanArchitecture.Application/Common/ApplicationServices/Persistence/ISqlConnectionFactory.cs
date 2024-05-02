using System.Data;

namespace CleanArchitecture.Application.Common.ApplicationServices.Persistence;

public interface ISqlConnectionFactory
{
    IDbConnection CreateConnection();
}
