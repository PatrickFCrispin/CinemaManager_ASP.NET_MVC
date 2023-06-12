using CinemaManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace CinemaManager.Models
{
    public class UserModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Informe o E-mail")]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Informe a Senha")]
        public string Password { get; set; } = default!;

        [Required(ErrorMessage = "Informe o Login")]
        public string Login { get; set; } = default!;

        [Required(ErrorMessage = "Selecione o Perfil")]
        public PerfilEnum Perfil { get; set; }

        public string? PerfilDescription { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool ValidatePasswordFor(string password)
        {
            return Password == password;
        }
    }
}