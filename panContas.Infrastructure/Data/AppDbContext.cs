using Microsoft.EntityFrameworkCore;
using panContas.Domain.Entities;

namespace panContas.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<PessoaFisica> PessoasFisicas { get; set; }
        public DbSet<PessoaJuridica> PessoasJuridicas { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            // Ensures mapping to postgres cleanly
            modelBuilder.Entity<PessoaFisica>().HasOne(p => p.Endereco).WithMany().HasForeignKey(p => p.EnderecoId);
            modelBuilder.Entity<PessoaJuridica>().HasOne(p => p.Endereco).WithMany().HasForeignKey(p => p.EnderecoId);
        }
    }
}
