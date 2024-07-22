using System.Collections.Generic;
using FreeKassa.NET;
using LogServices;
using NewWebApi.Exceptions.PayExceptions;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Models.Enum;
using NewWebApi.Repositories.Contracts;
using NewWebApi.Services.Contracts;
using Npgsql;

namespace NewWebApi.Services
{
	public class PaymentServices : IPaymentService
	{
		private readonly IFreeKassaService _freeKassaService;
		private readonly ILoggerManager _logger;
		private readonly IRepositoryManager _repositoryManager;

		public PaymentServices(IFreeKassaService freeKassaService, ILoggerManager logger, IRepositoryManager repositoryManager)
		{
			_freeKassaService = freeKassaService;
			_logger = logger;
			_repositoryManager = repositoryManager;
		}
		public async Task<IEnumerable<string>> CreatePayRefers(string orderId)
		{	
			_logger.LogInfo($"CreatePayRefers: {orderId}");
			return await CreateAndConcatenatePayLinks(orderId);	
		}
		public async Task<string> CreateOrder(string id)
		{
			var orderID = Guid.NewGuid().ToString();
			var result = await _repositoryManager.PaymentRepository.AddOrederInformation(orderID, id);
			if(result == 0)
				throw new OrderDidntCreateException(id);
			_logger.LogInfo($"CreateOrder: {id}");
			return orderID;
			
		}
		public async Task SuccessOrderStatus(string orderId, decimal amount)
		{	
			var responseCount = await ConvertPriceToResponse(amount);
			var result = await _repositoryManager.PaymentRepository.UpdatePaymentInformations(orderId, responseCount);
			if(result == 0)
				throw new FailToUpdatePaymentInforException(orderId);

			_logger.LogInfo($"SuccessOrderStatus: {orderId}");
			
		}
		public async Task DoWorkWhenNotificationSuccess(PaymentRequest paymentRequest)
		{
			var getSign = _freeKassaService.GetNotificationSign(paymentRequest.MERCHANT_ORDER_ID, paymentRequest.AMOUNT);
			if(getSign != paymentRequest.SIGN)
				throw new WrongSignEception(paymentRequest.MERCHANT_ORDER_ID);
			await SuccessOrderStatus(paymentRequest.MERCHANT_ORDER_ID, paymentRequest.AMOUNT);	
			await AddPaymenInformation(paymentRequest);
			_logger.LogInfo($"AddPaymenInformation for successful order {paymentRequest.MERCHANT_ORDER_ID}");
		}
		
		// public async Task<bool> CheckOrderStatus(string orderId)
		// {
		// 	using (var command = new NpgsqlCommand("SELECT status FROM orders WHERE orderId = @orderId;"))
		// 	{
		// 		command.Parameters.AddWithValue("@orderId", orderId);
		// 		var result = await _repository.Request(command, TypeOfComand.Get);
		// 		_logger.LogInfo($"CheckOrderStatus: {result}");
		// 		var status = result.DataTableResult.Rows[0]["status"].ToString();
		// 		if(status == "success")
		// 		{
		// 			return true;
		// 		}
		// 		else 
		// 		{
		// 			return false;
		// 		}
				
		// 	}
		// }
		public async Task FailOrderStatus(string orderId)
		{
			var result =await _repositoryManager.PaymentRepository.AddPaymenInformationAfterFail(orderId);
			if(result == 0)
				throw new FailAddPaymenInformationAfterFail(orderId);
			_logger.LogInfo($"FailOrderStatus: {orderId}");	
		}
		public async Task AddPaymenInformation(PaymentRequest paymentRequest)
		{	
			var result = await _repositoryManager.PaymentRepository.AddPaymenInformationAfterSuccess(paymentRequest);
			if(result == 0)
				throw new FailAddPaymenInformationAfterSuccess(paymentRequest.MERCHANT_ORDER_ID);
			
			_logger.LogInfo($"AddPaymenInformation for : {paymentRequest.MERCHANT_ORDER_ID}");
		}
		
		private async Task <IEnumerable<string>> CreateAndConcatenatePayLinks(string orderId)
		{
			var links = new List<string>()
			{
				_freeKassaService.GetPayLink(orderId, 99,"RUB"),
				_freeKassaService.GetPayLink(orderId, 299,"RUB"),
				_freeKassaService.GetPayLink(orderId, 499,"RUB")
			};
			
			if (links.Count != 3)
				throw new LinkDidntCreateException(orderId);
			
			_logger.LogInfo($"CreatePayLinks:  for {orderId}");
			return links;
		}
		private async Task<int> ConvertPriceToResponse(decimal price)
		{
			return price switch
			{
				99 => 1,
				299 => 5,
				499 => 10,
				_ => 0
			};

		} 
		
	}
}