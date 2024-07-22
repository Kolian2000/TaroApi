using LogServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.AuthModel;
using NewWebApi.Services.Contracts;

namespace NewWebApi.Services.Attributes
{
	public class AuthAtrtribute : Attribute, IAsyncActionFilter
	{
		public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
		{

			var head = context.HttpContext.Request.Headers;
			if (!head.TryGetValue("Id", out var headersValue))
			{
				throw new Exception("Header is empty or format is wrong");
			}

			var loggerManager = context.HttpContext.RequestServices.GetRequiredService<ILoggerManager>();
			loggerManager.LogInfo($"User: {headersValue} try to access: {context.HttpContext.Request.Path}");
			
			await next();
			
			
			var headers = context.HttpContext.Request.Headers;
			if (!headers.TryGetValue("Id", out var header))
			{
				return;
			}

			var serviceManager = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>();
			await serviceManager.UserService.DeductResponseCount(headersValue);
			
			
		}

		// public async void OnResourceExecuted(ResourceExecutedContext context)
		// { 
			
		// 	var headers = context.HttpContext.Request.Headers;
		// 	if (!headers.TryGetValue("Id", out var headersValue))
		// 	{
		// 		return;
		// 	}

		// 	var serviceManager = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>();
		// 	await serviceManager.UserService.DeductResponseCount(headersValue);
		// }

		// public async void OnResourceExecuting(ResourceExecutingContext context)
		// {
			
		// }
	}
}