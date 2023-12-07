using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string? Color { get; set; }
        public string? size { get; set; }
        public int  Quantity  { get; set; }
        public int productID { get; set; }
        [ForeignKey("productID")]
        public Product? Product { get; set; }
    }
}
