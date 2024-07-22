namespace NewWebApi.Exceptions.PromoCodeExceptions
{
    public class PromoCodeNotFoundException : NotFoundException
    {
        public PromoCodeNotFoundException(string message) : base(message)
        {
        }
    }
}