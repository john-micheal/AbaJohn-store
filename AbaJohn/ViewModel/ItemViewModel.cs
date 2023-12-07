using AbaJohn.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AbaJohn.ViewModel
{
    public class ItemViewModel
    {
        public int? ID { get; set; }
        [Required(ErrorMessage="Please Select Color")]
        public string Color { get; set; }
        public string? size { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity should be greater than or equal to 1")]
        public int Quantity { get; set; }
        public int productID { get; set; }
       
        public productViewModel Product { get; set; }

        public List<Colors_and_Sizes> Colors{ get; set; }
        public List<Colors_and_Sizes> Sizes{ get; set; }

        public List<Item>? Items { get; set; }
        public string? ReturnUrl { get; set; }

    }
}
