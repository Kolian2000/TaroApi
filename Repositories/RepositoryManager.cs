using NewWebApi.Repositories.Contracts;

namespace NewWebApi.Repositories
{
	public class RepositoryManager : IRepositoryManager
	{
		private readonly IDbConnectionFactory _dbConnectionFactory;
		private readonly Lazy<ICardRepository> _cardRepository;
		private readonly Lazy<IUserRepository> _userRepository;
		private readonly Lazy<IPromoCodeRepository> _promocodeRepository;
		private readonly Lazy<IPaymentRepository> _paymentRepository;
		

		public RepositoryManager(IDbConnectionFactory dbConnectionFactory)
		{
			_dbConnectionFactory = dbConnectionFactory;
			_cardRepository = new Lazy<ICardRepository>(() => new CardRepository(_dbConnectionFactory));
			_userRepository = new Lazy<IUserRepository>(() => new UserRepository(_dbConnectionFactory));
			_promocodeRepository = new Lazy<IPromoCodeRepository>(() => new PromoCodeRepository(_dbConnectionFactory));
			_paymentRepository = new Lazy<IPaymentRepository>(() => new PaymentRepository(_dbConnectionFactory));
		}

		public ICardRepository CardRepository => _cardRepository.Value;

		public IUserRepository UserRepository => _userRepository.Value;

		public IPromoCodeRepository PromoCodeRepository => _promocodeRepository.Value;

		public IPaymentRepository PaymentRepository => _paymentRepository.Value;
	}
}