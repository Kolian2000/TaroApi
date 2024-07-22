namespace NewWebApi.Models
{
    public class PromoCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool IsUsed { get; set; }
        public string UsedByUsername { get; set; }
        public DateTime? UsedAt { get; set; }
        public string CreatedByUsername { get; set; }
        public string PromoCodeType { get; set; }
    }

}