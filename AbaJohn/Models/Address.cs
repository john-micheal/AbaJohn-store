using System.ComponentModel.DataAnnotations.Schema;

namespace AbaJohn.Models
{
    public class Address
    {
        public int id { get; set; }

        public string country { get; set; }

        public string city { get; set; }

        public int home_number { get; set; }

        public string street_name { get; set; }


        public string User_id { get; set; }
        [ForeignKey("User_id")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}