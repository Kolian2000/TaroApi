using NewWebApi.Repositories.Contracts;
using Npgsql;
using Dapper;
using System.Runtime.InteropServices;

namespace NewWebApi.Repositories
{
	public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
	{
		protected readonly IDbConnectionFactory _dbConnectionFactory;

		public RepositoryBase(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory;
		}
		
		public async Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object> parameters)
		{
			using var connection = _dbConnectionFactory.CreateDbConnection();
			{
				await connection.OpenAsync();
				return await connection.ExecuteAsync(sql, parameters);	
			}
		} 

		public async Task<T> ExecuteScalarAsync<T>(string sql, Dictionary<string, object> parameters)
		{
			using var connection = _dbConnectionFactory.CreateDbConnection();
			{
				await connection.OpenAsync();
				return await connection.ExecuteScalarAsync<T>(sql, parameters);
			}
			
		}

		public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, Dictionary<string, object> parameters)
		{
			using var connection = _dbConnectionFactory.CreateDbConnection();
			{
				await connection.OpenAsync();
				return await connection.QueryAsync<T>(sql, parameters);	
			}
		}
	  
		

	}
}