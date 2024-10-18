﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Sistema_TechStore.Models.DB;

public partial class DbTechStoreContext : DbContext
{
    public DbTechStoreContext()
    {
    }

    public DbTechStoreContext(DbContextOptions<DbTechStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CategoriasProducto> CategoriasProductos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<ComprasEmpresa> ComprasEmpresas { get; set; }

    public virtual DbSet<DetallesCompra> DetallesCompras { get; set; }

    public virtual DbSet<DetallesVenta> DetallesVentas { get; set; }

    public virtual DbSet<Marca> Marcas { get; set; }

    public virtual DbSet<Modelo> Modelos { get; set; }

    public virtual DbSet<MovimientosInventario> MovimientosInventarios { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<Proveedore> Proveedores { get; set; }

    public virtual DbSet<TiposProducto> TiposProductos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<VentasEmpresa> VentasEmpresas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-1H52LMG; Database=dbTechStore; Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoriasProducto>(entity =>
        {
            entity.HasKey(e => e.IdCategoriaProducto).HasName("PK__Categori__876435014350950B");

            entity.ToTable("Categorias_Productos");

            entity.Property(e => e.IdCategoriaProducto).HasColumnName("id_categoria_producto");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoria");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__Clientes__677F38F5F2A7C646");

            entity.HasIndex(e => e.Telefono, "UQ__Clientes__2A16D945A1B45AE0").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Clientes__AB6E61646446A8D0").IsUnique();

            entity.HasIndex(e => e.Dui, "UQ__Clientes__D876F1BFB7835DDA").IsUnique();

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Direccion)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dui)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dui");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NombreCliente)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_cliente");
            entity.Property(e => e.Telefono)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<ComprasEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdCompra).HasName("PK__Compras___C4BAA60457FEDE97");

            entity.ToTable("Compras_Empresa");

            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.EstadoCompra)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("estado_compra");
            entity.Property(e => e.FechaCompra).HasColumnName("fecha_compra");
            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("id_usuario");
            entity.Property(e => e.TotalCompra)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("total_compra");

            entity.HasOne(d => d.IdProveedorNavigation).WithMany(p => p.ComprasEmpresas)
                .HasForeignKey(d => d.IdProveedor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_compraProveedor");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.ComprasEmpresas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_controlUsuarioCompra");
        });

        modelBuilder.Entity<DetallesCompra>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCompra).HasName("PK__Detalles__BD16E279CDFE68BA");

            entity.ToTable("Detalles_Compras");

            entity.Property(e => e.IdDetalleCompra)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("id_detalle_compra");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdCompra).HasColumnName("id_compra");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdCompraNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.IdCompra)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_compraDetalle");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallesCompras)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_productoCompraDetalle");
        });

        modelBuilder.Entity<DetallesVenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleVenta).HasName("PK__Detalles__5B265D4774FF7DD3");

            entity.ToTable("Detalles_Ventas");

            entity.Property(e => e.IdDetalleVenta)
                .HasMaxLength(7)
                .IsUnicode(false)
                .HasColumnName("id_detalle_venta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.PrecioUnitario)
                .HasColumnType("decimal(6, 2)")
                .HasColumnName("precio_unitario");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_productoVentaDetalle");

            entity.HasOne(d => d.IdVentaNavigation).WithMany(p => p.DetallesVenta)
                .HasForeignKey(d => d.IdVenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ventaDetalle");
        });

        modelBuilder.Entity<Marca>(entity =>
        {
            entity.HasKey(e => e.IdMarca).HasName("PK__Marcas__7E43E99E91892CE3");

            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.Marca1)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("marca");
        });

        modelBuilder.Entity<Modelo>(entity =>
        {
            entity.HasKey(e => e.IdModelo).HasName("PK__Modelos__B3BFCFF1A22A9996");

            entity.Property(e => e.IdModelo).HasColumnName("id_modelo");
            entity.Property(e => e.IdMarca).HasColumnName("id_marca");
            entity.Property(e => e.Modelo1)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("modelo");

            entity.HasOne(d => d.IdMarcaNavigation).WithMany(p => p.Modelos)
                .HasForeignKey(d => d.IdMarca)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_marcaModelo");
        });

        modelBuilder.Entity<MovimientosInventario>(entity =>
        {
            entity.HasKey(e => e.IdMovimiento).HasName("PK__Movimien__2A071C24AD6A39D5");

            entity.ToTable("Movimientos_Inventario");

            entity.Property(e => e.IdMovimiento).HasColumnName("id_movimiento");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.DescripcionMovimiento)
                .HasMaxLength(224)
                .IsUnicode(false)
                .HasColumnName("descripcion_movimiento");
            entity.Property(e => e.FechaMovimiento).HasColumnName("fecha_movimiento");
            entity.Property(e => e.IdProducto)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("id_usuario");
            entity.Property(e => e.TipoMovimiento)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("tipo_movimiento");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_productoMovimiento");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.MovimientosInventarios)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_controlUsuarioMovimiento");
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__FF341C0D0DCB2765");

            entity.Property(e => e.IdProducto)
                .HasMaxLength(8)
                .IsUnicode(false)
                .HasColumnName("id_producto");
            entity.Property(e => e.CantidadStock).HasColumnName("cantidad_stock");
            entity.Property(e => e.DescripcionProducto)
                .HasMaxLength(512)
                .IsUnicode(false)
                .HasColumnName("descripcion_producto");
            entity.Property(e => e.Estado)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.IdCategoriaProducto).HasColumnName("id_categoria_producto");
            entity.Property(e => e.IdModelo).HasColumnName("id_modelo");
            entity.Property(e => e.IdTipoProducto).HasColumnName("id_tipo_producto");
            entity.Property(e => e.NombreProducto)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombre_producto");
            entity.Property(e => e.PrecioCompra)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("precio_compra");
            entity.Property(e => e.PrecioVenta)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("precio_venta");

            entity.HasOne(d => d.IdCategoriaProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdCategoriaProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_categoriaProducto");

            entity.HasOne(d => d.IdModeloNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdModelo)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_modeloProducto");

            entity.HasOne(d => d.IdTipoProductoNavigation).WithMany(p => p.Productos)
                .HasForeignKey(d => d.IdTipoProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_tipoProducto");
        });

        modelBuilder.Entity<Proveedore>(entity =>
        {
            entity.HasKey(e => e.IdProveedor).HasName("PK__Proveedo__8D3DFE28E4B0868C");

            entity.HasIndex(e => e.Telefono, "UQ__Proveedo__2A16D9455546126B").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Proveedo__AB6E61642C1C07A8").IsUnique();

            entity.HasIndex(e => e.Dui, "UQ__Proveedo__D876F1BF926682E1").IsUnique();

            entity.Property(e => e.IdProveedor).HasColumnName("id_proveedor");
            entity.Property(e => e.Direccion)
                .HasMaxLength(120)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dui)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dui");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NombreProveedor)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre_proveedor");
            entity.Property(e => e.Telefono)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<TiposProducto>(entity =>
        {
            entity.HasKey(e => e.IdTipoProducto).HasName("PK__Tipos_Pr__F5E0BFB87791834E");

            entity.ToTable("Tipos_Productos");

            entity.Property(e => e.IdTipoProducto).HasColumnName("id_tipo_producto");
            entity.Property(e => e.TipoProducto)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("tipo_producto");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuarios__4E3E04AD8689EB50");

            entity.Property(e => e.IdUsuario)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("id_usuario");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("apellidos");
            entity.Property(e => e.Clave)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.Direccion)
                .HasMaxLength(128)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.NombreUsuario)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre_usuario");
            entity.Property(e => e.Nombres)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("nombres");
            entity.Property(e => e.Rol)
                .HasMaxLength(16)
                .IsUnicode(false)
                .HasColumnName("rol");
            entity.Property(e => e.Telefono)
                .HasMaxLength(24)
                .IsUnicode(false)
                .HasColumnName("telefono");
        });

        modelBuilder.Entity<VentasEmpresa>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__Ventas_E__459533BFA93A9C1F");

            entity.ToTable("Ventas_Empresa");

            entity.Property(e => e.IdVenta).HasColumnName("id_venta");
            entity.Property(e => e.EstadoVenta)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("estado_venta");
            entity.Property(e => e.FechaVenta).HasColumnName("fecha_venta");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdUsuario)
                .HasMaxLength(6)
                .IsUnicode(false)
                .HasColumnName("id_usuario");
            entity.Property(e => e.MetodoPago)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("metodo_pago");
            entity.Property(e => e.TotalVenta)
                .HasColumnType("decimal(7, 2)")
                .HasColumnName("total_venta");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.VentasEmpresas)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_ventaCliente");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.VentasEmpresas)
                .HasForeignKey(d => d.IdUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_controlUsuarioVenta");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
