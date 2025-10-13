namespace WebAppSuporteIA.Models
{
    public class Chamado
    {
        public int Id { get; set; }
    public string? UsuarioNome { get; set; }
    public string? Motivo { get; set; }
    public string? Status { get; set; }
    public string? Descricao { get; set; }
        public int? TecnicoId { get; set; }
        public string? TecnicoNome { get; set; }
    }
}
