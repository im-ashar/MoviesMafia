using System.ComponentModel.DataAnnotations;

namespace MoviesMafia.Models.Repo
{
    public class UserLogInModel
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
