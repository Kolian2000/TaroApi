namespace NewWebApi.Models
{
    public class UserUsedPromoCodeType
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PromoCodeType { get; set; }
        public DateTime UsedAt { get; set; }

    }
}