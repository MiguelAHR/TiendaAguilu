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

        public DbSet<Rol> Roles { get; set; }
        public DbSet<Persona> Personas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<AuxiliarAlmacen> AuxiliarAlmacen { get; set; }
        public DbSet<JefeAlmacen> JefeAlmacen { get; set; }
        public DbSet<Administrador> Administrador { get; set; }

        public DbSet<Kardex> Kardex { get; set; }
        public DbSet<TipoMovimiento> TipoMovimiento { get; set; }
        public DbSet<Inventario> Inventario { get; set; }

        public DbSet<TipoPago> TipoPago { get; set; }       
        public DbSet<Pedido> Pedido { get; set; }
        public DbSet<RegistroPago> RegistroPago { get; set; }
        public DbSet<DetallePedido> DetallePedido { get; set; }

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



            // Mapeo de las Entidades Usuario
            modelBuilder.Entity<Rol>().ToTable("rol");
            modelBuilder.Entity<Persona>().ToTable("persona");
            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.ToTable("usuario");
                // Configuración de la relación Usuario-Persona
                entity.HasOne(d => d.Persona)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.PersonaId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación Usuario-Rol
                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // Mapeo de las Entidades AuxiliarAlmacen
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<AuxiliarAlmacen>(entity =>
            {
                entity.ToTable("auxiliarAlmacen");
                // Configuración de la relación AuxiliarA-Usuario
                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.AuxiliarAlmacens)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapeo de las Entidades JefeAlmacen
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<JefeAlmacen>(entity =>
            {
                entity.ToTable("jefeAlmacen");
                // Configuración de la relación JefeA-Usuario
                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.JefeAlmacens)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapeo de las Entidades Administrador
            modelBuilder.Entity<Usuario>().ToTable("usuario");
            modelBuilder.Entity<Administrador>(entity =>
            {
                entity.ToTable("administrador");
                // Configuración de la relación Admin-Usuario
                entity.HasOne(d => d.Usuario)
                    .WithMany(p => p.Administradors)
                    .HasForeignKey(d => d.UsuarioId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapeo de las Entidades Kardex
            modelBuilder.Entity<TipoMovimiento>().ToTable("tipoMovimiento");
            modelBuilder.Entity<Inventario>().ToTable("inventario");
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<JefeAlmacen>().ToTable("jefeAlmacen");
            modelBuilder.Entity<Kardex>(entity =>
            {
                entity.ToTable("kardex");
                // Configuración de la relación Kardex-TipMov
                entity.HasOne(d => d.TipoMovimiento)
                    .WithMany(p => p.Kardexs)
                    .HasForeignKey(d => d.TipoMovId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación Kardex-Iventario
                entity.HasOne(d => d.Inventario)
                    .WithMany(p => p.Kardexs)
                    .HasForeignKey(d => d.InventarioId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación Kardex-Productos
                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Kardexs)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación Kardex-JefeAlmacen
                entity.HasOne(d => d.JefeAlmacen)
                    .WithMany(p => p.Kardexs)
                    .HasForeignKey(d => d.jefeAlmacenId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapeo de las Entidades Pedido
            modelBuilder.Entity<Proveedor>().ToTable("proveedores");
            modelBuilder.Entity<Producto>().ToTable("productos");
            modelBuilder.Entity<Administrador>().ToTable("administrador");
            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.ToTable("pedido");
                // Configuración de la relación pedido-proveedor
                entity.HasOne(d => d.Proveedor)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.ProveedorId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación pedido-Productos
                entity.HasOne(d => d.Producto)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.ProductoId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación pedido-Administrador
                entity.HasOne(d => d.Administrador)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Mapeo de las Entidades RegistroPago
            modelBuilder.Entity<TipoPago>().ToTable("tipoPago");
            modelBuilder.Entity<Administrador>().ToTable("administrador");
            modelBuilder.Entity<Pedido>().ToTable("pedido");
            modelBuilder.Entity<RegistroPago>(entity =>
            {
                entity.ToTable("registroPago");
                // Configuración de la relación RegistroPago-TipoPago
                entity.HasOne(d => d.TipoPago)
                    .WithMany(p => p.RegistroPagos)
                    .HasForeignKey(d => d.TipoPagoId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación RegistroPago-Admin
                entity.HasOne(d => d.Administrador)
                    .WithMany(p => p.RegistroPagos)
                    .HasForeignKey(d => d.AdminId)
                    .OnDelete(DeleteBehavior.Cascade);
                // Configuración de la relación RegistroPago-Pedido
                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.RegistroPagos)
                    .HasForeignKey(d => d.PedidoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            // Mapeo de las Entidades DetallePedido
            modelBuilder.Entity<Pedido>().ToTable("pedido");
            modelBuilder.Entity<DetallePedido>(entity =>
            {
                entity.ToTable("detallepedido");
                // Configuración de la relación DetallePedido-Pedido
                entity.HasOne(d => d.Pedido)
                    .WithMany(p => p.DetallePedidos)
                    .HasForeignKey(d => d.PedidoId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(modelBuilder);
        }

    }
}
