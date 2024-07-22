using NewWebApi.Repositories.Contracts;
using NewWebApi.Repositories;
using NewWebApi.Services.Contracts;
using NewWebApi.Services;

namespace NewWebApi.Extensions
{
	public static class ServiceExtensions
	{
		public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
		public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();
	}
}