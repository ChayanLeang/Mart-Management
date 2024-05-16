namespace SayuJapnShop.Models
{
    public class Export
    {
        public int ExportId { get; set; }
        public int InvoiceId { get; set; }
        public int ItemID { get; set; }
        public int Price { get; set; }
        public int Qty { get; set; }
        public int Amount { get; set; }
        public virtual Invoice Invoice { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}

