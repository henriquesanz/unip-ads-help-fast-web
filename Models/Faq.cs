using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSuporteIA.Models
{
    [Table("Faqs", Schema = "dbo")]
    public class Faq
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Pergunta { get; set; } = string.Empty;
        [Required]
        public string Resposta { get; set; } = string.Empty;
    }
}
