
using NewWebApi.Repositories.Contracts;
using NewWebApi.Models.AuthModel;
using Npgsql;
using NewWebApi.Models;

namespace NewWebApi.Repositories;

public class UserRepository : RepositoryBase<User>, IUserRepository
{
	public UserRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
	{
	}
	
	public async Task<User> CheckUserExists(string name)
	{
		var sql = "SELECT * FROM \"users\" WHERE username = @username;";
		
		var parameters = new Dictionary<string, object>()
		{
			{"@username", name}
		};
		var result = await ExecuteQueryAsync<User>(sql, parameters);
		
		return result.FirstOrDefault();
		
	}

	public async Task<int> AddUser(User user)
	{
		var sqlQuery = @"
				INSERT INTO ""users"" (username, password_hash)
				SELECT @username, @password_hash
				WHERE NOT EXISTS (SELECT 1 FROM ""users"" WHERE username = @username);";
		var parametrs = new Dictionary<string, object>()
		{
			{"@username", user.UserName},
			{"@password_hash", user.User_Id}
		};
		return await ExecuteNonQueryAsync(sqlQuery, parametrs);	
	}

	public async Task<int> CheckResponseCount(string name)
	{
		var sqlQuery = "SELECT response_count FROM users WHERE username = @username;";
		var parametrs = new Dictionary<string, object>()
		{
			{"@username", name}
		};
		return await ExecuteScalarAsync<int>(sqlQuery, parametrs);
	}
	public async Task<int> UpdateResponseCount(string userName)
	{
		var sqlQuery = "UPDATE users SET response_count = response_count + 2 WHERE username = @username;";
		var parametrs = new Dictionary<string, object>()
		{
			{"@username", userName}
		};
		return await ExecuteNonQueryAsync(sqlQuery, parametrs);
	}
	public async Task<int> RecordUserPromoCodeTypeUsage( string userName,string promoCodeType)
	{
		var sqlQuery = @"INSERT INTO UserUsedPromoCodeTypes (username, PromoCodeType) 
													VALUES (@username, @promoCodeType);";
		var Dictionary = new Dictionary<string, object>()
		{
			{"@username", $"{userName}"} ,
			{"@promoCodeType", promoCodeType}
		};
		return await ExecuteNonQueryAsync(sqlQuery, Dictionary);											
	}
	public async Task DeductResponseCount(string userName)
	{
		var sqlQuery = "UPDATE users SET response_count = response_count - 1 WHERE username = @username;";
		var parametrs = new Dictionary<string, object>()
		{
			{"@username", userName}
		};
		await ExecuteNonQueryAsync(sqlQuery, parametrs);
	}
	
}