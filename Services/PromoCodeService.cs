using System.Text.RegularExpressions;
using LogServices;
using NewWebApi.Exceptions.PromoCodeExceptions;
using NewWebApi.Models.AuthModel;
using NewWebApi.Repositories.Contracts;
using NewWebApi.Services.Contracts;
using Powells.CouponCode;
using Quartz.Util;

namespace NewWebApi.Services
{
	public class PromoCodeService : IPromoCodeService
	{
		private readonly IRepositoryManager _repositoryManager;
		private readonly ILoggerManager _logger;

		public PromoCodeService(IRepositoryManager repositoryManager, ILoggerManager logger)
		{
			_repositoryManager = repositoryManager;
			_logger = logger;
		}
		public async Task<string> CreatePromocode(string userName)
		{
			var opt = new Powells.CouponCode.Options();
			var ccb = new CouponCodeBuilder();
			
			var bad = ccb.BadWordsList;
			var code = ccb.Generate(opt);
			var result = await _repositoryManager.PromoCodeRepository.CreatedPromocode(code, userName);
			
			if(result == 0)
				throw new Exception("Failed to create promo code.");
			return code;
		}
		public async Task ValidatePromoCodes(UserDto user)
		{
			string pattern = @"^[A-Z0-9]{4}-[A-Z0-9]{4}-[A-Z0-9]{4}$";
			if (!Regex.IsMatch(user.PromoCode, pattern))
				throw new InvalidPromoCodeFormatException(user.PromoCode);
				
			var opt = new Powells.CouponCode.Options();
			var ccb = new CouponCodeBuilder();
			var validate = ccb.Validate(user.PromoCode, opt);
			
			if(validate.IsNullOrWhiteSpace())
				throw new InvalidPromoCodeException(user.PromoCode);
			var result = await _repositoryManager.PromoCodeRepository.GetPromocode(user.PromoCode);
			if(result == null)
				throw new PromoCodeNotFoundException(user.PromoCode);
			
			if(result.CreatedByUsername == user.Name)
				throw new Exception("You cannot use your own code.");
			if(await _repositoryManager.PromoCodeRepository.
			CheckUserUsedPromoCodeType(user.Name, result.PromoCodeType) != null)
				throw new Exception("You have already used this tipe of code.");
			await _repositoryManager.UserRepository.UpdateResponseCount(user.Name);
			await _repositoryManager.UserRepository.UpdateResponseCount(result.CreatedByUsername);
			await _repositoryManager.UserRepository.RecordUserPromoCodeTypeUsage(user.Name, result.PromoCodeType);
		}
	}
}