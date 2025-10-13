using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppSuporteIA.Models;
using WebAppSuporteIA.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppSuporteIA.Pages
{
    public class VerTodosChamadosModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;
    public List<HistoricoChamado> Chamados { get; set; } = new();
    public List<Usuario> Tecnicos { get; set; } = new();

        public VerTodosChamadosModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task OnGetAsync()
        {
            Chamados = await _usuarioService.ListarHistoricoChamadosAsync();
            Tecnicos = await _usuarioService.ListarUsuariosPorCargoAsync("Tecnico");
        }
    }
}
