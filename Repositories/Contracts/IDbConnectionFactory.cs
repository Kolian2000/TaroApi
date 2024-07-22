using Npgsql;

namespace NewWebApi.Repositories.Contracts
{
    public interface IDbConnectionFactory
    {
        NpgsqlConnection CreateDbConnection();
    }
}