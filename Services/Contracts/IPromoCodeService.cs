using NewWebApi.Models.AuthModel;

namespace NewWebApi.Services.Contracts
{
	public interface IPromoCodeService
	{
		 Task<string> CreatePromocode(string userName);
		 Task ValidatePromoCodes(UserDto user);
	}
}