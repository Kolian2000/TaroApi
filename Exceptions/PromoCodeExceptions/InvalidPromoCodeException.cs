namespace NewWebApi.Exceptions.PromoCodeExceptions
{
    public class InvalidPromoCodeException : Exception
    {
        public InvalidPromoCodeException(string message) : base($"Invalid promo code - {message}. The code provided does not pass the checksum validation.")
        {
        }
    }
}