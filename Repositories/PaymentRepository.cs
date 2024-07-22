using NewWebApi.Models;
using NewWebApi.Repositories.Contracts;

namespace NewWebApi.Repositories
{
	public class PaymentRepository : RepositoryBase<Order> ,IPaymentRepository
	{

		public PaymentRepository(IDbConnectionFactory dbConnectionFactory) : base(dbConnectionFactory)
		{
		}
		
		public async Task<int> AddOrederInformation(string orderId, string id)
		{
			var sqlQuery = @"
			INSERT INTO orders (orderId, status, username, timeCreate)
			VALUES (@orderId, @status, @username, @timeCreate);
			";

			
			var parametrs = new Dictionary<string, object>()
			{
				{"@orderId", orderId},
				{"@status", "pending"},
				{"@username", id},
				{"@timeCreate", DateTime.Now}
			};
			return await ExecuteNonQueryAsync(sqlQuery, parametrs);
		}
		public async Task<int> UpdatePaymentInformations(string orderId, int responseCount)
		{
			var sqlQuery = $@"
			UPDATE orders
			SET status = 'success'
			WHERE OrderId = @orderId;

			UPDATE users
			SET response_count = response_count + {responseCount}
			WHERE username = (
				SELECT orders.username
				FROM orders
				WHERE OrderId = @orderId
			);
			";

			var parametrs = new Dictionary<string, object>()
			{
				{"@orderId", orderId}
			};
			return await ExecuteNonQueryAsync(sqlQuery, parametrs);

		}
		public async Task<int> AddPaymenInformationAfterSuccess(PaymentRequest paymentRequest)
		{
			var sqlQuery = @"
			UPDATE orders 
			SET (amount, intid) 
			VALUES(@amount, @intid)
			WHERE orderId = @orderId;
			";

			var parametrs = new Dictionary<string, object>()
			{
				{"@amount", paymentRequest.AMOUNT},
				{"@intid", paymentRequest.intid},
				{"@orderId", paymentRequest.MERCHANT_ORDER_ID}
			};
			return await ExecuteNonQueryAsync(sqlQuery, parametrs);
		}
		public async Task<int> AddPaymenInformationAfterFail(string orderId)
		{
			var sqlQuery = @"
			UPDATE orders 
			SET status = @status 
			WHERE orderId = @orderId;";

			var parametrs = new Dictionary<string, object>()
			{
				{"@status", "fail"},
				{"@orderId", orderId}
			};
			return await ExecuteNonQueryAsync(sqlQuery, parametrs);
		}
		
	}
}