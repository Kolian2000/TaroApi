using NewWebApi.Interface;
using NewWebApi.Models.Enum;
using Npgsql;

namespace NewWebApi.Models
{
	public class Desc
	{
		public int? Id { get; set; }
		public string Desc_Name { get; set; }
		public string? Desc_Kind { get; set; }
		public string Question { get; set; }
		
	}
}