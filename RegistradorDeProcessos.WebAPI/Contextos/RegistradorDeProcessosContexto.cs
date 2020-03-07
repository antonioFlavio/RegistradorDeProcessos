using Microsoft.EntityFrameworkCore;
using RegistradorDeProcessos.WebAPI.Model;

namespace RegistradorDeProcessos.WebAPI.Contextos
{
    public class RegistradorDeProcessosContexto : DbContext
    {
        public RegistradorDeProcessosContexto(DbContextOptions<RegistradorDeProcessosContexto> options) : base(options)
        {
        }

        public DbSet<Processo> Processos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Processo>().HasKey(m => m.Numero);
            modelBuilder.Entity<Processo>().HasMany(m => m.Movimentacoes);

            base.OnModelCreating(modelBuilder);
        }
    }
}
