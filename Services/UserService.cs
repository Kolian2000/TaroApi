using System.Text.RegularExpressions;
using LogServices;
using Microsoft.AspNetCore.Http.HttpResults;
using NewWebApi.Exceptions;
using NewWebApi.Models.AuthModel;
using NewWebApi.Repositories.Contracts;
using NewWebApi.Services.Contracts;
using Powells.CouponCode;
using Quartz.Util;

namespace NewWebApi.Services
{
	public class UserService : IUserService
	{
		private readonly IRepositoryManager _repositoryManager;
		private readonly ILoggerManager _logger;

		public UserService(IRepositoryManager repositoryManager, ILoggerManager logger)
		{
			_repositoryManager = repositoryManager;
			_logger = logger;
		}
		public async Task<User> CheckUserExist(string userName)
		{
			var users = await _repositoryManager.UserRepository.CheckUserExists(userName);
			if(users is null)
				throw new UserNotFoundException(userName);
			return users;	
		}
		public async Task AddUser(User user)
		{
			var result = await _repositoryManager.UserRepository.AddUser(user);
			if(result == 0)
				throw new UserAlreadyExistsException(user.UserName);	
		}
		public async Task<int> CheckResponseCount(string userName)
		{
			return await _repositoryManager.UserRepository.CheckResponseCount(userName);		
		}

		public async Task DeductResponseCount(string userName)
		{
			await _repositoryManager.UserRepository.DeductResponseCount(userName);       
		}
	}
}