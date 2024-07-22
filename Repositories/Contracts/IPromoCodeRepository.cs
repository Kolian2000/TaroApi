using NewWebApi.Models;

namespace NewWebApi.Repositories.Contracts
{
	public interface IPromoCodeRepository
	{
		 Task<int> CreatedPromocode(string code, string userName);
		 Task<PromoCode> GetPromocode(string code);
		 Task<UserUsedPromoCodeType> CheckUserUsedPromoCodeType(string userName, string promoCodeType);
	}
}