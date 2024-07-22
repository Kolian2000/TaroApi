namespace NewWebApi.Exceptions.PayExceptions
{
    public class LinkDidntCreateException : Exception
    {
        public LinkDidntCreateException(string message) : base($"Failed to create payment link for orderId - {message}.")
        {  
        }
    }
}