using Microsoft.EntityFrameworkCore;
using Phibra.Prova.Domain.Entities;

namespace Phibra.Prova.Data
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<Movimentacao> Movimentacao { get; set; }
        public DbSet<TipoMovimentacao> TipoMovimentacao { get; set; }
       
    }
}