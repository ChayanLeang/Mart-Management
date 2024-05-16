namespace SayuJapnShop.Models
{
    public class VImport
    {
        public int ImportId { get; set; }
        public string ItemName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public int Qty { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public int Remains { get; set; }
        public string Date { get; set; } = null!;
    }
}
