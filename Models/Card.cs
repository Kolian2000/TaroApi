namespace NewWebApi.Models
{
	public class Card
	{	
		public int Card_Id { get; set; }
		public string Card_Name { get; set; }
		public int _Number { get; set; }
		public string Suit { get; set; }
		public bool Is_Arcane { get; set; }
		public string PicturePath { get; set; }
		public string fk_Desc_name { get; set; }
	}
}