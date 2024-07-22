using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using BCrypt.Net;
using LogServices;
using Microsoft.AspNetCore.Mvc;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.AuthModel;
using NewWebApi.Services.Attributes;
using NewWebApi.Services.Contracts;
using Quartz.Util;

namespace NewWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly ILoggerManager _logger;
		private readonly IServiceManager _serviceManager;

		public AuthController( ILoggerManager logger, IServiceManager serviceManager)
		{
			_logger = logger;
			_serviceManager = serviceManager;
		}
		[HttpPost("Register")]
		public async Task<ActionResult> Register(User user)
		{	
			await _serviceManager.UserService.AddUser(user);
			return Ok("User created");
		}
		[HttpPost("CheckUser")]
		public async Task<ActionResult> CheckUser([FromBody]string name)
		{
			var result = await _serviceManager.UserService.CheckUserExist(name);
			return Ok(result);
		}
		
		[HttpPost("Response")]
		public async Task<ActionResult> CheckResponse([FromBody]string userName)
		{
			var result = await _serviceManager.UserService.CheckResponseCount(userName);
			return Ok(result);
		}
		[HttpPost("GetPromoCode")]
		public async Task<IActionResult> Coupon([FromBody]UserDto userName)
		{
			return Ok(await _serviceManager.PromoCodeService.CreatePromocode(userName.Name));
		
		}
		[HttpPost("ValidatePromoCode")]
		public async Task<IActionResult> ValidatePromoCode([FromBody]UserDto userName)
		{
			await _serviceManager.PromoCodeService.ValidatePromoCodes(userName);
			return Ok();
		}
		
		
		
	}
}