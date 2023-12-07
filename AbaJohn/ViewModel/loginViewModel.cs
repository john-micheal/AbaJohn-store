using System.ComponentModel.DataAnnotations;

namespace AbaJohn.ViewModel
{
    public class loginViewModel
    {
        /*    [Required]
            [DataType(DataType.EmailAddress)]
            public string email { get; set; }*/
        [Required]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }



        public bool rememberme { get; set; }



    }
}
