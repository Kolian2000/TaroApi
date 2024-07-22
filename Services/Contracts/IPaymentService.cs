using NewWebApi.Models;

namespace NewWebApi.Services.Contracts
{
	public interface IPaymentService
	{
		public Task SuccessOrderStatus(string orderId, decimal amount);

		public Task  FailOrderStatus(string orderId);
		// public Task<bool> CheckOrderStatus(string orderId);
		public Task<string> CreateOrder(string id);
		public Task<IEnumerable<string>> CreatePayRefers(string orderId);
		public Task AddPaymenInformation(PaymentRequest paymentRequest);
		Task DoWorkWhenNotificationSuccess(PaymentRequest paymentRequest);
		
	}
}