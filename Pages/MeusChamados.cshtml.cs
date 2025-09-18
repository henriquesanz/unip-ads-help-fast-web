using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebAppSuporteIA.Pages;

public class MeusChamadosModel : PageModel
{
    public List<ChamadoViewModel> Chamados { get; set; } = new();

    public void OnGet()
    {
        // TODO: Implementar busca de chamados do usuário
        // Por enquanto, usando dados mockados
        Chamados = new List<ChamadoViewModel>
        {
            new ChamadoViewModel
            {
                Id = 1,
                Titulo = "Problema com acesso ao sistema",
                Descricao = "Não consigo acessar o sistema principal. Recebo erro 404 ao tentar fazer login.",
                Status = "Aberto",
                Prioridade = "Alta",
                DataCriacao = DateTime.Now.AddDays(-2)
            },
            new ChamadoViewModel
            {
                Id = 2,
                Titulo = "Solicitação de nova funcionalidade",
                Descricao = "Gostaria de solicitar a implementação de relatórios em PDF para o módulo de vendas.",
                Status = "Em Andamento",
                Prioridade = "Média",
                DataCriacao = DateTime.Now.AddDays(-5)
            }
        };
    }
}

public class ChamadoViewModel
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Prioridade { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }
}
