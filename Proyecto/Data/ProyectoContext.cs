using Microsoft.EntityFrameworkCore;
using Proyecto.Models;


namespace Proyecto.Data
{
    public class ProyectoContext : DbContext
    {
        public ProyectoContext(DbContextOptions<ProyectoContext> options) : base(options) { }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<Proveedor> Proveedor { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mapeo de la entidad Proveedor a la tabla proveedores
            modelBuilder.Entity<Proveedor>().ToTable("proveedores");

            // Mapeo de la entidad Categoria a la tabla categorias (si es necesario)
            modelBuilder.Entity<Categoria>().ToTable("categorias");

            // Mapeo de la entidad Producto a la tabla productos
            modelBuilder.Entity<Producto>(entity =>
            {
                entity.ToTable("productos");

                // Configuración de la relación Producto-Categoria
                entity.HasOne(d => d.Categoria)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.CategoriaId)
                    .OnDelete(DeleteBehavior.Cascade);

                // Configuración de la relación Producto-Proveedor
                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Productos)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }


    }
}
