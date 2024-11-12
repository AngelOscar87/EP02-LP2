using Microsoft.EntityFrameworkCore;
using EP02_LP2.Models;

namespace EP02_LP2.Data
{
    public class TenantDbContext : DbContext
    {
        public TenantDbContext(DbContextOptions<TenantDbContext> options)
            : base(options)
        {
        }

        // DbSet para Apartments
        public DbSet<Apartment> Apartments { get; set; }

        // DbSet para ElectricityConsumption
        public DbSet<ElectricityConsumption> ElectricityConsumptions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de relaciones y restricciones
            modelBuilder.Entity<ElectricityConsumption>()
                .HasOne(ec => ec.Apartment) // Relación con Apartment
                .WithMany() // Sin navegación inversa (opcional)
                .HasForeignKey(ec => ec.ApartmentId) // Clave foránea
                .OnDelete(DeleteBehavior.Cascade); // Elimina consumos si el apartamento se elimina
        }
    }
}
