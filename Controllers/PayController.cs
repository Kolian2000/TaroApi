using FreeKassa.NET;
using LogServices;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.AuthModel;
using NewWebApi.Services.Contracts;
using Powells.CouponCode;

namespace NewWebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PayController : ControllerBase
	{
		
		private readonly IServiceManager _serviceManager;
		private readonly IWebHostEnvironment _webHostEnvironment;

        public PayController(IWebHostEnvironment webHostEnvironment, IServiceManager serviceManager)
        {
            
            _webHostEnvironment = webHostEnvironment;
            _serviceManager = serviceManager;
        }

        [HttpPost("SuccessNotification")]
		public async Task<IActionResult> HandleSuccessNotification([FromForm]PaymentRequest request)
		{
			await _serviceManager.PaymentService.DoWorkWhenNotificationSuccess(request);
			return Ok();
		}
		[HttpPost("GetPayLink")]
		public async Task<IActionResult> GetPayLink([FromBody]string orderId)
		{	
			return Ok(_serviceManager.PaymentService.CreatePayRefers(orderId));
		}
		[HttpPost("GetOrder")]
		public async Task<IActionResult> GetOrder([FromBody]string id)
		{
			
			return Ok(await _serviceManager.PaymentService.CreateOrder(id));
		}
		// [HttpPost("CheckStatus")]
		// public async Task<IActionResult> CheckStatus([FromBody]string orderId)
		// {
		// 	_logger.LogInfo($"CheckStatus: {orderId} ");
		// 	return Ok(await _options.CheckOrderStatus(orderId));
		// }
		[HttpGet("Success")]
		public async Task<IActionResult> Success()
		{	
			var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/System/Success.html");
			if (System.IO.File.Exists(path))
			{
				return PhysicalFile(path, "text/html");
			}
			return NotFound();
		}

		[HttpGet("Error")]
		public async Task<IActionResult> Error()
		{
			var path = Path.Combine(_webHostEnvironment.WebRootPath, "Images/System/Unsuccess.html");
			if (System.IO.File.Exists(path))
			{
				return PhysicalFile(path, "text/html");
			}
			return NotFound();
		}
		
	}
}