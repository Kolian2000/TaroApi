using NewWebApi.Repositories.Contracts;
using NewWebApi.Models;

namespace NewWebApi.Repositories
{
	public class CardRepository : RepositoryBase<Card>, ICardRepository
	{
		public CardRepository(IDbConnectionFactory dbConnectionFactory)
		: base(dbConnectionFactory)
		{
		}
		public async Task<IEnumerable<Card>> GetCards(string descName)
		{
			var sqlQuery = @"
			SELECT * 
			FROM card 
			WHERE fk_desc_name = @descName 
			ORDER BY RANDOM() LIMIT 3;";
			var parametrs = new Dictionary<string, object>()
			{
				{"@descName", descName}
			};
			return await ExecuteQueryAsync<Card>(sqlQuery, parametrs);
		}
	}
}