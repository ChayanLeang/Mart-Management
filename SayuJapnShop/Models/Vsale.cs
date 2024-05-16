namespace SayuJapnShop.Models
{
    public class Vsale
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public int Price { get; set; }

        public int Qty { get; set; }

        public int Amount { get; set; }
    }
}
