using NewWebApi.Repositories.Contracts;
using Npgsql;

namespace NewWebApi.DataAccess
{
	public class DbConnectionFactory : IDbConnectionFactory
	{
        private readonly string _configuration;

        public DbConnectionFactory(IConfiguration configuration)
		{
            _configuration = configuration.GetConnectionString("DefaultConnection");
        }
		public NpgsqlConnection CreateDbConnection()
		{
			return new NpgsqlConnection(_configuration);
		}
	}
}