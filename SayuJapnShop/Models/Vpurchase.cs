namespace SayuJapnShop.Models
{
    public class Vpurchase
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public int Cost { get; set; }

        public int Price { get; set; }

        public int Qty { get; set; }

        public string Date { get; set; } = null!;
        public string status { get; set; } = null!;
    }
}
