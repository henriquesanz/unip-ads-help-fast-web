using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebAppSuporteIA.Services;
using static WebAppSuporteIA.Models.UserRoleExtensions;

namespace WebAppSuporteIA.Pages;

public class LoginModel : PageModel
{
    private readonly IUsuarioService _usuarioService;

    public LoginModel(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [BindProperty]
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    public void OnGet()
    {
        // Página de login carregada
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            // Buscar usuário real no banco
            var usuario = await _usuarioService.ObterPorEmailAsync(Email);
            if (usuario == null)
            {
                ModelState.AddModelError("", "Usuário não encontrado. Verifique o e-mail informado.");
                return Page();
            }

            // Validar credenciais reais
            if (usuario.Senha != Password)
            {
                ModelState.AddModelError("", "Senha incorreta. Tente novamente.");
                return Page();
            }

            // Atualizar último login
            await _usuarioService.AtualizarUltimoLoginAsync(usuario.Id);

            // Armazenar dados na TempData
            TempData["UserId"] = usuario.Id;
            TempData["UserName"] = usuario.Nome;
            TempData["UserEmail"] = usuario.Email;
            TempData["UserType"] = usuario.Cargo.Nome;

            return RedirectToPage("/Dashboard");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Ocorreu um erro ao fazer login. Tente novamente.");
            return Page();
        }
    }
}
