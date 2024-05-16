using System.ComponentModel.DataAnnotations;

namespace SayuJapnShop.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string CustomerName { get; set; } = null!;

        [Required(ErrorMessage = "Enter Phone")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "Choose Gender")]
        public string Gender { get; set; } = null!;

        [Required(ErrorMessage = "Enter Address")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "Choose Status")]
        public string status { get; set; } = null!;

        public virtual ICollection<Invoice> Invoices { get; } = new List<Invoice>();
        //public virtual ICollection<Export> Exports { get; } = new List<Export>();
    }
}

/*
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



 public int ImportId { get; set; }

        public string ItemName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;

        public string Date { get; set; } = null!;

        public int Price { get; set; }

        public int Cost { get; set; }

        public int Qty { get; set; }

        public int Remains { get; set; }
 */
