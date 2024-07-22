namespace NewWebApi.Exceptions.PayExceptions
{
    public class FailAddPaymenInformationAfterFail : Exception
    {
        public FailAddPaymenInformationAfterFail(string message) : base($"Failed to add payment information after fail for orderId - {message}.")
        {	
        }
    }
}