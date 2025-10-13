using System.ComponentModel.DataAnnotations;
namespace WebAppSuporteIA.Models;

public class Usuario
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Email { get; set; } = string.Empty;

    [Required]
    [StringLength(15)]
    public string Telefone { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Senha { get; set; } = string.Empty;

    [Required]
    public int CargoId { get; set; }
    public Cargo Cargo { get; set; } = null!;
}
