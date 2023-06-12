using CinemaManager.Enums;
using System.ComponentModel.DataAnnotations;

namespace CinemaManager.Models
{
    public class SessionModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe a Data da sessão")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Informe o Horário do início do filme")]
        public DateTime StartTime { get; set; }
        
        public DateTime EndTime { get; set; }

        [Required(ErrorMessage = "Informe o Valor do ingresso")]
        public string TicketValue { get; set; } = default!;

        [Required(ErrorMessage = "Informe o Tipo de animação")]
        public TypeOfAnimationEnum TypeOfAnimation { get; set; }
        
        public string? TypeOfAnimationDescription { get; set; }

        [Required(ErrorMessage = "Informe o Tipo de áudio")]
        public AudioTypeEnum AudioType { get; set; }
        
        public string? AudioTypeDescription { get; set; }

        [Required(ErrorMessage = "Informe o Título do filme")]
        public string MovieTitle { get; set; } = default!;

        [Required(ErrorMessage = "Informe o Nome da sala")]
        public string TheaterName { get; set; } = default!;

        public DateTime CreatedAt { get; set; }
    }
}