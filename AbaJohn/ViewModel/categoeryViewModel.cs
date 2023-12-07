using System.ComponentModel.DataAnnotations;

namespace AbaJohn.ViewModel
{
    public class categoeryViewModel
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public string type { get; set; }
    }
}
