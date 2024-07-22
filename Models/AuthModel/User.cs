using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NewWebApi.Interface;
using NewWebApi.Models.Enum;
using Npgsql;
using Powells.CouponCode;
using Quartz.Util;

namespace NewWebApi.Models.AuthModel
{
	public class User
	{
		[Column("user_id")]
		public int? User_Id { get; set; }
		[Column("username")]
		[Required]
		public string UserName { get; set; }
		[Column("password_hash")]
		public string? Password_Hash { get; set; }
		[Column("email")]
		public string? Email { get; set; }
		[Column("response_count")]
		public int? Response_Count { get; set; }
		[Column("created_at")]
		public DateTime? Created_At { get; set; }
		[Column("can_get_horoscope")]
		public bool? Can_Get_Horoscope { get; set; }

	}
}