using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppSuporteIA.Models
{
    [Table("Chats", Schema = "dbo")]
    public class Chat
    {
        [Key]
        public int Id { get; set; }
        public int ChamadoId { get; set; }
        [Required]
        public string Mensagem { get; set; } = string.Empty;
        public bool EnviadoPorCliente { get; set; }
        public DateTime DataEnvio { get; set; }
        [ForeignKey("ChamadoId")]
        public Chamado Chamado { get; set; } = null!;
    }
}
