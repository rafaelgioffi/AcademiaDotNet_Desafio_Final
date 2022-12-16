using Microsoft.EntityFrameworkCore;

namespace SistemaDeEncomendas.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Encomendas> Encomendas { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {         
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Encomendas>()
                .Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");
        }
    }
}
