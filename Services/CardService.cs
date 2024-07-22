using LogServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Exceptions;
using NewWebApi.Models;
using NewWebApi.Repositories.Contracts;
using NewWebApi.Services.Contracts;

namespace NewWebApi.Services
{
	public class CardService : ICardService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly ILoggerManager loggerManager;

		public CardService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
		{
			this.repositoryManager = repositoryManager;
			this.loggerManager = loggerManager;
		}
		
		public async Task<IEnumerable<Card>> GetCards(string descName)
		{
			var result = await repositoryManager.CardRepository.GetCards(descName);
			if(result is null)
				throw new CardsNotFoudException(descName);
			return result;
		}
	}
}