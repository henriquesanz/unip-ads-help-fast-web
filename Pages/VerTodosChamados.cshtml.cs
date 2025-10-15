using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppSuporteIA.Models;
using WebAppSuporteIA.Data;
using WebAppSuporteIA.Services;
using Microsoft.EntityFrameworkCore;

namespace WebAppSuporteIA.Pages
{
    public class VerTodosChamadosModel : PageModel
    {
        public List<Chamado> Chamados { get; set; } = new();
        public List<HistoricoChamado> HistoricoChamados { get; set; } = new();
        public List<Usuario> Tecnicos { get; set; } = new();

        private readonly ApplicationDbContext _context;
        private readonly IUsuarioService _usuarioService;

        public VerTodosChamadosModel(ApplicationDbContext context, IUsuarioService usuarioService)
        {
            _context = context;
            _usuarioService = usuarioService;
        }

        public async Task OnGetAsync()
        {
            Chamados = await _context.Chamados
                .Include(c => c.Cliente)
                .ToListAsync();

            HistoricoChamados = await _usuarioService.ListarHistoricoChamadosAsync();
            Tecnicos = await _usuarioService.ListarUsuariosPorCargoAsync("Tecnico");
        }
    }
}
