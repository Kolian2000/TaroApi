using LogServices;
using NewWebApi.Repositories.Contracts;
using NewWebApi.Services.Contracts;

namespace NewWebApi.Services
{
	public class DescService : IDescService
	{
		private readonly IRepositoryManager repositoryManager;
		private readonly ILoggerManager loggerManager;

		public DescService(IRepositoryManager repositoryManager, ILoggerManager loggerManager)
		{
			this.repositoryManager = repositoryManager;
			this.loggerManager = loggerManager;
		}
		
	}
}