using NewWebApi.Models.AuthModel;

namespace NewWebApi.Services.Contracts
{
	public interface IUserService
	{
		Task<User> CheckUserExist(string userName);
		Task AddUser(User user);
		Task<int> CheckResponseCount(string userName);
		Task DeductResponseCount(string userName);
	}
}