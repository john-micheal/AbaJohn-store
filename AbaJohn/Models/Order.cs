using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double TotalPrice { get; set; }
        public DateTime date { get; set; }

        public string User_id { get; set; }
        [ForeignKey("User_id")]
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Product> products {get; set;}
/*        [ForeignKey("ProductID")]
        public int ProductID { get; set; }
        public Product product { get; set; }
*/
        public Payment payment{ get; set; }
    }
}
