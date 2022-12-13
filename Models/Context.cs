using Microsoft.EntityFrameworkCore;

namespace SistemaDeEncomendas.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<Encomendas> Encomendas { get; set; }
        public DbSet<Clientes> Clientes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Encomendas>()
                .HasOne(c => c.Clientes)
                .WithMany(e => e.Encomendas)
                .HasForeignKey(c => c.ClientesId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Encomendas>()
                .Property(e => e.Valor)
                .HasColumnType("decimal(18,2)");
        }
    }
}
