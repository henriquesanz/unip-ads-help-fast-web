using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebAppSuporteIA.Models;
using WebAppSuporteIA.Services;

namespace WebAppSuporteIA.Pages;

public class DashboardModel : PageModel
{
    private readonly IUsuarioService _usuarioService;

    public DashboardModel(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    public string UserName { get; set; } = "Usuário";
    public UserRole UserType { get; set; } = UserRole.Cliente;
    public string UserTypeDisplay { get; set; } = "Cliente";
    public string UserTypeDescription { get; set; } = "";

    public async Task OnGetAsync()
    {
        // Obter dados do usuário da TempData (temporário até implementar sessão)
        var userId = TempData["UserId"]?.ToString();
        if (int.TryParse(userId, out var id))
        {
            var usuario = await _usuarioService.ObterPorIdAsync(id);
            if (usuario != null)
            {
                UserName = usuario.Nome;
                UserType = usuario.TipoUsuario;
                UserTypeDisplay = usuario.TipoUsuarioDisplay;
                UserTypeDescription = usuario.TipoUsuarioDescription;
            }
        }
        else
        {
            UserName = TempData["UserName"]?.ToString() ?? "Usuário";
        }
        
        // Manter os dados na TempData para outras páginas
        TempData.Keep("UserName");
        TempData.Keep("UserId");
        TempData.Keep("UserEmail");
    }
}
