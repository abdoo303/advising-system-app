// Add a using statement for DataAnnotations
using System.ComponentModel.DataAnnotations;

namespace BookQWeb.Models.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public int id { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

