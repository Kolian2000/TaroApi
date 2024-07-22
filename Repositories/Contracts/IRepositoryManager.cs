namespace NewWebApi.Repositories.Contracts
{
	public interface IRepositoryManager
	{
		ICardRepository CardRepository { get; }
		IUserRepository UserRepository { get; }
		IPromoCodeRepository PromoCodeRepository { get; }
		IPaymentRepository PaymentRepository { get; }
		
	}
}