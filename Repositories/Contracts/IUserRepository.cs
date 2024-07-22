using NewWebApi.Models.AuthModel;

namespace NewWebApi.Repositories.Contracts
{
	public interface IUserRepository
	{
		Task<User> CheckUserExists(string name);
		Task<int> AddUser(User user);
		Task<int> CheckResponseCount(string name);
		Task<int> UpdateResponseCount(string userName);
		Task<int> RecordUserPromoCodeTypeUsage( string userName,string promoCodeType);
		Task DeductResponseCount(string userName);

	}
}