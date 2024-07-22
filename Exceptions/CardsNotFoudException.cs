namespace NewWebApi.Exceptions
{
    public class CardsNotFoudException : NotFoundException
    {
        public CardsNotFoudException(string message) : base($"Card from the provided desc -{message} not found")
        {
        }
    }
}