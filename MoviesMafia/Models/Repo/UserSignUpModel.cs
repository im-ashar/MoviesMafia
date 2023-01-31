using System.ComponentModel.DataAnnotations;

namespace MoviesMafia.Models.Repo
{
    public class UserSignUpModel
    {

        [Required]
        [StringLength(20, ErrorMessage = "Length Should Not Exceed 20 Characters")]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirmation Password Doesn't Match")]
        public string ConfirmPassword { get; set; }
    }
}

