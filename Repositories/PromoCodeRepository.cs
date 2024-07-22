using NewWebApi.Models;
using NewWebApi.Repositories.Contracts;

namespace NewWebApi.Repositories
{
	public class PromoCodeRepository : RepositoryBase<PromoCode>, IPromoCodeRepository
	{
		public PromoCodeRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
		{
		}
		public async Task<int> CreatedPromocode(string code, string userName)
		{
			var sqlQuery = @"INSERT INTO PromoCodes (Code,Description, ExpiryDate,PromoCodeType,CreatedByUsername,UsedByUsername)
				VALUES (@code, @description,@expiryDate,@promoCodeType, @CreatedByUsername,@UsedByUsername);";
			var parametrs = new Dictionary<string, object>()
			{
				{"@code", code},
				{"@description", "For share"},
				{"@expiryDate", DateTime.Now.AddYears(1)},
				{"@promoCodeType", "friend"},
				{"@CreatedByUsername", userName},
				{"@UsedByUsername", "default"}
					
			};
			return await ExecuteNonQueryAsync(sqlQuery, parametrs);	
				
		}
		public async Task<PromoCode> GetPromocode(string code)
		{
			var sqlQuery = "SELECT * FROM PromoCodes WHERE Code = @code;";
			
			var parametrs = new Dictionary<string, object>()
			{
				{"@code", code}
			};
			
			var result = await ExecuteQueryAsync<PromoCode>(sqlQuery,parametrs);

			return result.FirstOrDefault();
		}
		public async Task<UserUsedPromoCodeType> CheckUserUsedPromoCodeType(string userName, string promoCodeType)
		{
			var sqlQuery = @"SELECT * 
			FROM UserUsedPromoCodeTypes 
			WHERE Username = @username AND PromoCodeType = @promoCodeType LIMIT 1;";

			var parametrs = new Dictionary<string, object>()
			{
				{"@username", $"{userName}"} ,
				{"@promoCodeType", $"{promoCodeType}"}
			};
			
			var result = await ExecuteQueryAsync<UserUsedPromoCodeType>(sqlQuery,parametrs);
			
			return result.FirstOrDefault();
			
		}
	
	}
}