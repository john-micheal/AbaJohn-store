using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class CartItem
    {
        public int Id { get; set; } 
        public int ProdectCount { get; set; }

        public ICollection<Product> products { get; set; }
        public string UserId { get; set;}
        [ForeignKey("UserId")]
        public ApplicationUser User { get; set;}
    }
}
