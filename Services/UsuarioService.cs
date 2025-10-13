using Microsoft.EntityFrameworkCore;
using WebAppSuporteIA.Models;
using WebAppSuporteIA.Data;

namespace WebAppSuporteIA.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly ApplicationDbContext _context;

        public UsuarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorEmailAsync(string email)
        {
            return await _context.Usuarios.Include(u => u.Cargo)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> ObterPorIdAsync(int id)
        {
            return await _context.Usuarios.Include(u => u.Cargo)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario> CriarUsuarioAsync(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<bool> ValidarLoginAsync(string email, string senha)
        {
            var usuario = await ObterPorEmailAsync(email);
            if (usuario == null)
                return false;
            return usuario.Senha == senha;
        }

        public async Task AtualizarUltimoLoginAsync(int usuarioId)
        {
            await Task.CompletedTask;
        }

        public async Task<bool> EmailExisteAsync(string email)
        {
            return await _context.Usuarios.AnyAsync(u => u.Email == email);
        }

        public async Task<Usuario> CriarClienteAsync(Usuario cliente)
        {
            cliente.CargoId = await ObterCargoIdPorNomeAsync("Cliente");
            return await CriarUsuarioAsync(cliente);
        }

        public async Task<Usuario> CriarTecnicoAsync(Usuario tecnico, int criadoPorId)
        {
            if (string.IsNullOrWhiteSpace(tecnico.Nome) || string.IsNullOrWhiteSpace(tecnico.Email) || string.IsNullOrWhiteSpace(tecnico.Senha))
                throw new ArgumentException("Todos os campos são obrigatórios para registrar um técnico.");

            var emailExistente = await _context.Usuarios.AnyAsync(u => u.Email == tecnico.Email);
            if (emailExistente)
                throw new ArgumentException("Já existe um usuário com este e-mail.");

            tecnico.CargoId = await ObterCargoIdPorNomeAsync("Tecnico");
            return await CriarUsuarioAsync(tecnico);
        }

        public async Task<Usuario> CriarAdministradorAsync(Usuario administrador, int criadoPorId)
        {
            administrador.CargoId = await ObterCargoIdPorNomeAsync("Administrador");
            return await CriarUsuarioAsync(administrador);
        }

        public async Task<List<Usuario>> ListarUsuariosPorCargoAsync(string cargoNome)
        {
            return await _context.Usuarios.Include(u => u.Cargo)
                .Where(u => u.Cargo.Nome == cargoNome)
                .OrderBy(u => u.Nome)
                .ToListAsync();
        }

        public async Task<int> ObterCargoIdPorNomeAsync(string cargoNome)
        {
            var cargo = await _context.Cargos.FirstOrDefaultAsync(c => c.Nome == cargoNome);
            if (cargo == null) throw new ArgumentException($"Cargo '{cargoNome}' não encontrado.");
            return cargo.Id;
        }

        public async Task<List<HistoricoChamado>> ListarHistoricoChamadosAsync()
        {
            return await _context.HistoricoChamados.ToListAsync();
        }
    }
}