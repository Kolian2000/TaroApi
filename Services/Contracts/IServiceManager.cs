namespace NewWebApi.Services.Contracts
{
	public interface IServiceManager
	{
		IDescService DescService { get; }
		IUserService UserService { get; }
		IPromoCodeService PromoCodeService { get; }
		ICardService CardService { get; }
		IPaymentService PaymentService { get; }
		
	}
}