using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace WebAppSuporteIA.Pages;

public class NovoChamadoModel : PageModel
{
    [BindProperty]
    [Required(ErrorMessage = "O título é obrigatório")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "O título deve ter entre 5 e 100 caracteres")]
    public string Titulo { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "A descrição é obrigatória")]
    [StringLength(1000, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 1000 caracteres")]
    public string Descricao { get; set; } = string.Empty;

    [BindProperty]
    [Required(ErrorMessage = "A prioridade é obrigatória")]
    public string Prioridade { get; set; } = string.Empty;

    public void OnGet()
    {
        // Página de novo chamado carregada
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        // TODO: Implementar lógica de criação de chamado
        // Por enquanto, redireciona para o dashboard com mensagem de sucesso
        TempData["SuccessMessage"] = "Chamado criado com sucesso!";
        return RedirectToPage("/Dashboard");
    }
}
