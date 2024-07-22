namespace NewWebApi.Models
{
    public class Taro
    {
        public IEnumerable<Card> Cards { get; set; }
        public IEnumerable<string> Answer { get; set; }
    }
}