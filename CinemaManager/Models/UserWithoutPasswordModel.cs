using CinemaManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace CinemaManager.Models
{
    public class UserWithoutPasswordModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        public string Name { get; set; } = default!;

        [Required(ErrorMessage = "Informe o E-mail")]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Informe o Login")]
        public string Login { get; set; } = default!;

        [Required(ErrorMessage = "Selecione o Perfil")]
        public PerfilEnum Perfil { get; set; }
    }
}