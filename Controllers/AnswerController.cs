using System.Data;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using LogServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.OpenApi;
using NewWebApi.Interface;
using NewWebApi.Models;
using NewWebApi.Services.Attributes;
using NewWebApi.Services.Contracts;
using Quartz.Util;

namespace NewWebApi.Controllers
{	[ApiController]
	[Route("api/[controller]")]
	public class AnswerController : ControllerBase
	{
		private readonly IOpenAiService _openAi;
		
        public AnswerController(IOpenAiService openai)
        {
            _openAi = openai; 
        }

        [HttpPost("GetTaroResponse")]
		// [AuthAtrtribute]
		public async Task<IActionResult> GetTaroResponse(Desc desc)
		{
			return Ok(await _openAi.GetResponseFromOpenAi(desc));	
		}
		
	}


}