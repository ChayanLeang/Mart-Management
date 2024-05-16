namespace SayuJapnShop.Models
{
    public class Vexport
    {
        public int ExportId { get; set; }
        public int InvoiceId { get; set; }
        public string ItemName { get; set; } = null!;

        public string CustomerName { get; set; } = null!;

        public string Date { get; set; } = null!;

        public int Price { get; set; }

        public int Qty { get; set; }
        public int Amount { get; set; }
    }
}
