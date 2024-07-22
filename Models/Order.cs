namespace NewWebApi.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string Status { get; set; }
        public DateTime TimeCreate { get; set; }
        public int? IntId { get; set; }
        public decimal? Amount { get; set; }
        public string UserName { get; set; }
    }
}