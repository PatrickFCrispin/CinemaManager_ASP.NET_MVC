using System.ComponentModel.DataAnnotations;

namespace CinemaManager.Models
{
    public class AuthModel
    {
        [Required(ErrorMessage = "Informe o Login")]
        public string Login { get; set; } = default!;

        [Required(ErrorMessage = "Informe a Senha")]
        public string Password { get; set; } = default!;
    }
}