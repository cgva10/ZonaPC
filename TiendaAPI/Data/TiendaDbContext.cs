using Microsoft.EntityFrameworkCore;
using TiendaAPI.Models;

public class TiendaDbContext : DbContext
{
    public TiendaDbContext(DbContextOptions<TiendaDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<ProductoImagen> ProductoImagenes { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<DetallePedido> DetallesPedido { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<CarritoProducto> CarritoProductos { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CarritoProducto>()
            .HasKey(cp => new { cp.CarritoId, cp.ProductoId });

        modelBuilder.Entity<Carrito>()
            .HasMany(c => c.Productos)
            .WithOne(cp => cp.Carrito)
            .HasForeignKey(cp => cp.CarritoId);

        modelBuilder.Entity<CarritoProducto>()
            .HasOne(cp => cp.Producto)
            .WithMany(p => p.CarritoProductos)
            .HasForeignKey(cp => cp.ProductoId);

        modelBuilder.Entity<Producto>()
            .Property(p => p.Precio)
            .HasPrecision(18, 2);

        modelBuilder.Entity<ProductoImagen>()
            .HasOne(pi => pi.Producto)
            .WithMany(p => p.Imagenes)
            .HasForeignKey(pi => pi.ProductoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Pedido>()
            .Property(p => p.Estado)
            .HasConversion<string>();


    }
}
