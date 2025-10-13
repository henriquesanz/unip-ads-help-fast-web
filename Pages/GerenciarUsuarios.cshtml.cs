using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppSuporteIA.Models;
using WebAppSuporteIA.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAppSuporteIA.Pages
{
    public class GerenciarUsuariosModel : PageModel
    {
        private readonly IUsuarioService _usuarioService;
        public List<Usuario> Clientes { get; set; } = new();
        public List<Usuario> Tecnicos { get; set; } = new();
        [BindProperty]
        public string Nome { get; set; } = string.Empty;
        [BindProperty]
        public string Email { get; set; } = string.Empty;
        [BindProperty]
        public string Telefone { get; set; } = string.Empty;
        [BindProperty]
        public string Senha { get; set; } = string.Empty;
        public string? MensagemSucesso { get; set; }

        public GerenciarUsuariosModel(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task OnGetAsync()
        {
            Clientes = await _usuarioService.ListarUsuariosPorCargoAsync("Cliente");
            Tecnicos = await _usuarioService.ListarUsuariosPorCargoAsync("Tecnico");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var tipoUsuario = TempData["UserType"]?.ToString();
            var userId = TempData["UserId"]?.ToString();
            // Só permite se for exatamente 'Administrador' (case-insensitive)
            if (string.IsNullOrEmpty(tipoUsuario) || !tipoUsuario.Equals("Administrador", System.StringComparison.OrdinalIgnoreCase) || string.IsNullOrEmpty(userId))
            {
                MensagemSucesso = "Apenas administradores podem registrar um técnico. Faça login como administrador para continuar.";
                await OnGetAsync();
                return Page();
            }

            int adminId = int.Parse(userId);

            var tecnico = new Usuario
            {
                Nome = Nome,
                Email = Email,
                Senha = Senha,
                Telefone = Telefone,
                CargoId = await _usuarioService.ObterCargoIdPorNomeAsync("Tecnico")
            };

            try
            {
                await _usuarioService.CriarTecnicoAsync(tecnico, adminId);
                MensagemSucesso = "Técnico registrado com sucesso";
            }
            catch (System.ArgumentException ex)
            {
                MensagemSucesso = ex.Message;
            }
            catch
            {
                MensagemSucesso = "Erro ao registrar técnico. Tente novamente ou verifique os dados.";
            }
            await OnGetAsync();
            return Page();
        }
    }
}
