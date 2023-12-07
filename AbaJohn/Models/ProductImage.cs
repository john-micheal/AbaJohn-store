using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class ProductImage
    {
        public int Id { get; set; }

        public string BaseImg { get; set; }
        public string Img1 { get; set; }
        public string Img2 { get; set; }
        public string Img3 { get; set; }

        public int Product_id { get; set; }
        [ForeignKey("Product_id")]
        public Product product { get; set; }
    }
}
