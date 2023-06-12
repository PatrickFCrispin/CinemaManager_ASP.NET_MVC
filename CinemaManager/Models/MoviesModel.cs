using System.ComponentModel.DataAnnotations;

namespace CinemaManager.Models
{
    public class MoviesModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o link da imagem")]
        public string Image { get; set; } = default!;

        [Required(ErrorMessage = "Informe o Título")]
        public string Title { get; set; } = default!;

        [Required(ErrorMessage = "Informe a Descrição")]
        public string Description { get; set; } = default!;

        [Required(ErrorMessage = "Informe a Duração")]
        public string Duration { get; set; } = default!;

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}