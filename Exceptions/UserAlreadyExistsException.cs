namespace NewWebApi.Exceptions
{
    public class UserAlreadyExistsException : Exception
    {
        public UserAlreadyExistsException(string username) 
            : base($"User with the provided username {username} already exists.")
        {
        }
    }
}