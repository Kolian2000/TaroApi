using NewWebApi.Models;

namespace NewWebApi.Repositories.Contracts
{
	public interface IPaymentRepository
	{
		Task<int> AddOrederInformation(string orderId, string id);
		Task<int> UpdatePaymentInformations(string orderId, int responseCount);
		Task<int> AddPaymenInformationAfterSuccess(PaymentRequest paymentRequest);
		Task<int> AddPaymenInformationAfterFail(string orderId);
	}
}