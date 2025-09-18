using Microsoft.EntityFrameworkCore;
using WebAppSuporteIA.Models;

namespace WebAppSuporteIA.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configuração do modelo Usuario
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Telefone).IsRequired().HasMaxLength(15);
            entity.Property(e => e.Senha).IsRequired().HasMaxLength(255);
            entity.Property(e => e.TipoUsuario).IsRequired().HasConversion<int>();
            entity.Property(e => e.DataCriacao).IsRequired();
            entity.Property(e => e.Ativo).IsRequired();

            // Índice único para email
            entity.HasIndex(e => e.Email).IsUnique();

            // Relacionamento auto-referencial para CriadoPor
            entity.HasOne(e => e.CriadoPor)
                  .WithMany()
                  .HasForeignKey(e => e.CriadoPorId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Dados iniciais para teste
        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Nome = "Administrador Master",
                Email = "admin@helpfast.com",
                Telefone = "(11) 99999-9999",
                Senha = "123456", // Em produção, deve ser hash
                TipoUsuario = UserRole.Administrador,
                DataCriacao = DateTime.Now,
                Ativo = true,
                CriadoPorId = null // Usuário master não foi criado por ninguém
            }
        );
    }
}
