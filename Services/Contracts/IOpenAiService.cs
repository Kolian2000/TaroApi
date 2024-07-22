using System.Data;
using NewWebApi.Models;

namespace NewWebApi.Services.Contracts
{
	public interface IOpenAiService
	{
		Task<Taro> GetResponseFromOpenAi(Desc desc);
	}
}