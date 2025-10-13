using System;
using System.ComponentModel.DataAnnotations;

namespace WebAppSuporteIA.Models;

public class HistoricoChamado
{
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Motivo { get; set; } = string.Empty;
    [Required]
    public DateTime DataAbertura { get; set; }
    public DateTime? DataFechamento { get; set; }
    [Required]
    [StringLength(30)]
    public string Status { get; set; } = string.Empty;

    // Relacionamento com Usuario
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
}
