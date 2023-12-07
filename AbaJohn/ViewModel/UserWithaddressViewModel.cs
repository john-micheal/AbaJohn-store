using AbaJohn.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace AbaJohn.ViewModel
{
    public class UserWithaddressViewModel
    {
      /*  public string Id { get; set; }*/

        public string user_name { get; set; }
        [DataType(DataType.Text)]
        public string name { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }



        [DataType(DataType.ImageUrl)]
        public IFormFile? ImageFile { get; set; }
        public string? image { get; set; }


        [Required]
        [Range(15, 100)]
        public int age { get; set; }

        [Required]
        public string Gender { get; set; }



        [Required]
        [DataType(DataType.PhoneNumber)]
        [MinLength(11)]
        public string phone_number { get; set; }
   
/*        public Address  address { get; set; }
*/        public int id { get; set; }

        public string country { get; set; }

        public string city { get; set; }

        public int home_number { get; set; }

        public string street_name { get; set; }

    }
}
