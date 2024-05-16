namespace SayuJapnShop.Models
{
    public class Import
    {
        public int ImportId { get; set; }

        public int ItemID { get; set; }

        public int SupplierID { get; set; }

        public int Price { get; set; }

        public int Cost { get; set; }
        public int Qty { get; set; }
        public string Date { get; set; } = null!;
        public int Remains { get; set; }
        public virtual Supplier Supplier { get; set; } = null!;
        public virtual Item Item { get; set; } = null!;
    }
}
