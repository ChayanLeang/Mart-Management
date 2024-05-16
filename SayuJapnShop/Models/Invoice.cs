namespace SayuJapnShop.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }

        public string Date { get; set; } = null!;

        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        public virtual ICollection<Export> Exports { get; } = new List<Export>();
    }
}
