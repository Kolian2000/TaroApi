namespace NewWebApi.Exceptions.PromoCodeExceptions
{
	public class InvalidPromoCodeFormatException : Exception
	{
		public InvalidPromoCodeFormatException(string message)
		:base($"The shape of promo code {message} is not valid.")  { }
	}
}