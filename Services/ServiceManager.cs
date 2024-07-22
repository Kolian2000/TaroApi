using FreeKassa.NET;
using LogServices;
using NewWebApi.Repositories.Contracts;
using NewWebApi.Services.Contracts;

namespace NewWebApi.Services
{
	public class ServiceManager : IServiceManager
	{
		private readonly Lazy<IUserService> _userService;
		private readonly Lazy<IDescService> _descService;
		private readonly Lazy<IPromoCodeService> _promoCodeService;
		private readonly Lazy<ICardService> _cardService;
		private readonly Lazy<IPaymentService> _paymentService;
		public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager loggerManager, IFreeKassaService freeKassaService
		)
		{
			_descService = new Lazy<IDescService>(() => new DescService(repositoryManager, loggerManager));
			_userService = new Lazy<IUserService>(() => new UserService(repositoryManager, loggerManager));
			_promoCodeService = new Lazy<IPromoCodeService>(() => new PromoCodeService(repositoryManager, loggerManager));
			_cardService = new Lazy<ICardService>(() => new CardService(repositoryManager, loggerManager));
			_paymentService = new Lazy<IPaymentService>(() => new PaymentServices(freeKassaService, loggerManager, repositoryManager));
		}

		public IUserService UserService => _userService.Value;
		public IDescService DescService => _descService.Value;
		public IPromoCodeService PromoCodeService => _promoCodeService.Value;

		public ICardService CardService => _cardService.Value;

		public IPaymentService PaymentService => _paymentService.Value;
	}
}