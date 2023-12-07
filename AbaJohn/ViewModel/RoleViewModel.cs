using System.ComponentModel.DataAnnotations;

namespace AbaJohn.ViewModel
{
    public class RoleViewModel
    {
        [Required]
        public string RoleName { get; set; }

    }
}