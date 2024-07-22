namespace NewWebApi.Exceptions.PayExceptions
{
    public class FailToUpdatePaymentInforException : Exception
    {
        public FailToUpdatePaymentInforException(string message) : base($"Failed to update payment information for orderId - {message}.")
        {
        }
    }
}