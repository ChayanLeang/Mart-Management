namespace SayuJapnShop.Models
{
    public class Vitem
    {
        public int ItemId { get; set; }

        public string ItemName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public string Descriptions { get; set; } = null!;
        public string status { get; set; } = null!;
    }
}
