using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using WebAppSuporteIA.Models;
using WebAppSuporteIA.Services;

namespace WebAppSuporteIA.Pages;

public class RegisterModel : PageModel
{
    private readonly IUsuarioService _usuarioService;

    public RegisterModel(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
    }

    [BindProperty]
    [Required(ErrorMessage = "O nome é obrigatório")]
    [MinLength(2, ErrorMessage = "O nome deve ter pelo menos 2 caracteres")]
    public string Name { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "O e-mail é obrigatório")]
    [EmailAddress(ErrorMessage = "Digite um e-mail válido")]
    public string Email { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "O telefone é obrigatório")]
    [Phone(ErrorMessage = "Digite um telefone válido")]
    [StringLength(15, MinimumLength = 10, ErrorMessage = "O telefone deve ter entre 10 e 15 caracteres")]
    public string Phone { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "A senha é obrigatória")]
    [MinLength(6, ErrorMessage = "A senha deve ter pelo menos 6 caracteres")]
    public string Password { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "A confirmação de senha é obrigatória")]
    [Compare("Password", ErrorMessage = "As senhas não coincidem")]
    public string ConfirmPassword { get; set; } = string.Empty;

    public void OnGet()
    {
        // Página de cadastro carregada
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // Verificar se o email já existe
        if (await _usuarioService.EmailExisteAsync(Email))
        {
            ModelState.AddModelError(nameof(Email), "Este e-mail já está cadastrado.");
            return Page();
        }

        try
        {
            // Criar novo cliente (auto-cadastro)
            var novoCliente = new Usuario
            {
                Nome = Name,
                Email = Email,
                Telefone = Phone,
                Senha = Password, // TODO: Implementar hash da senha
                DataCriacao = DateTime.Now,
                Ativo = true
            };

            await _usuarioService.CriarClienteAsync(novoCliente);

            TempData["SuccessMessage"] = "Cadastro realizado com sucesso! Faça login para continuar.";
            return RedirectToPage("/Login");
        }
        catch (Exception)
        {
            ModelState.AddModelError("", "Ocorreu um erro ao criar sua conta. Tente novamente.");
            return Page();
        }
    }
}
