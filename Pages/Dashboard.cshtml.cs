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

    public Usuario Usuario { get; set; } = null!;

    public async Task OnGetAsync()
    {
        // Obter dados do usuário da TempData (temporário até implementar sessão)
        var userId = TempData["UserId"]?.ToString();
        if (int.TryParse(userId, out var id))
        {
            Usuario = await _usuarioService.ObterPorIdAsync(id) ?? throw new Exception("Usuário não encontrado");
            UserName = Usuario.Nome;
            UserTypeDisplay = Usuario.Cargo.Nome;
            if (Enum.TryParse<UserRole>(Usuario.Cargo.Nome, out var role))
            {
                UserType = role;
            }
            UserTypeDescription = ""; // Ajuste conforme necessário
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
