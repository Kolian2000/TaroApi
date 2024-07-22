namespace NewWebApi.Exceptions
{
	public class UserNotFoundException : NotFoundException
	{
		public UserNotFoundException(string username) 
			: base($"User with the provided username {username} does not exist.")
		{
		}
	}
}