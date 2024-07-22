namespace NewWebApi.Exceptions.PayExceptions
{
    public class WrongSignEception : Exception
    {
        public WrongSignEception(string message) : base($"Signture is invalid for the provided orderId -{message}.")
        {	
        }
    }
}