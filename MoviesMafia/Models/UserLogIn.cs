using System.ComponentModel.DataAnnotations;

namespace MoviesMafia.Models
{
    public class UserLogIn
    {
        [Required]
        [StringLength(20)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
