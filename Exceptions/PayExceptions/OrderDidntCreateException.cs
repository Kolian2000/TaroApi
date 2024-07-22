namespace NewWebApi.Exceptions.PayExceptions
{
    public class OrderDidntCreateException : Exception
    {
        public OrderDidntCreateException(string message) : base($"Failed to create order for  - {message}.")
        {
            
        }
    }
}