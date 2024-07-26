using Microsoft.EntityFrameworkCore;
using MyCompu.Models;

namespace MyCompu.Data
{
    public class MyCompuDbContext : DbContext
    {
        public MyCompuDbContext (DbContextOptions<MyCompuDbContext> options) : base(options) { }

        public DbSet<Producto> Productos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de la relación entre Usuario y Producto
            modelBuilder.Entity<Usuario>()
                .HasMany(u => u.Productos)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
