using System.ComponentModel.DataAnnotations;

namespace WebAppSuporteIA.Models;

public class Cargo
{
    public int Id { get; set; }
    [Required]
    [StringLength(20)]
    public string Nome { get; set; } = string.Empty;
    public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
