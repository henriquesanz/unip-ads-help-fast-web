using Microsoft.EntityFrameworkCore;
using WebAppSuporteIA.Models;

namespace WebAppSuporteIA.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<HistoricoChamado> HistoricoChamados { get; set; }

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
            entity.HasIndex(e => e.Email).IsUnique();
            entity.HasOne(e => e.Cargo)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(e => e.CargoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Configuração do modelo Cargo
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(20);
            entity.HasIndex(e => e.Nome).IsUnique();
        });

        // Configuração do modelo HistoricoChamado
        modelBuilder.Entity<HistoricoChamado>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Motivo).IsRequired().HasMaxLength(255);
            entity.Property(e => e.DataAbertura).IsRequired();
            entity.Property(e => e.Status).IsRequired().HasMaxLength(30);
            entity.HasOne(e => e.Usuario)
                .WithMany()
                .HasForeignKey(e => e.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // Dados iniciais para teste
        modelBuilder.Entity<Cargo>().HasData(
            new Cargo { Id = 1, Nome = "Administrador" },
            new Cargo { Id = 2, Nome = "Tecnico" },
            new Cargo { Id = 3, Nome = "Cliente" }
        );

        modelBuilder.Entity<Usuario>().HasData(
            new Usuario
            {
                Id = 1,
                Nome = "Administrador Master",
                Email = "admin@helpfast.com",
                Telefone = "(11) 99999-9999",
                Senha = "123456", // Em produção, deve ser hash
                CargoId = 1 // Supondo que o cargo Administrador tem Id = 1
            }
        );
    }
}
