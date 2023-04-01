using System.ComponentModel.DataAnnotations;

namespace MoviesMafia.Models.Repo
{
    public class UpdateUserModel
    {
        [Required]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "New Password and Confirmation Password Do Not Match.")]
        public string ConfirmPassword { get; set; }
    }
}
