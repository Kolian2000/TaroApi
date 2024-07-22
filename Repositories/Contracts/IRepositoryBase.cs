using Npgsql;

namespace NewWebApi.Repositories.Contracts
{
	public interface IRepositoryBase<T>
	{
		Task<int> ExecuteNonQueryAsync(string sql, Dictionary<string, object> parameters = null);
		Task<T> ExecuteScalarAsync<T>(string sql, Dictionary<string, object> parameters = null);
		Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, Dictionary<string, object> parameters = null);
		
	}
}