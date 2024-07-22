namespace NewWebApi.Exceptions.PayExceptions
{
    public class FailAddPaymenInformationAfterSuccess : Exception
    {
        public FailAddPaymenInformationAfterSuccess(string message) : base($"Failed to add payment information after success for orderId - {message}.")
        {
        	
        }
    }
}