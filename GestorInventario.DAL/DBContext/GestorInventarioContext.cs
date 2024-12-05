using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using GestorInventario.Model;

namespace GestorInventario.DAL.DBContext;

public partial class GestorInventarioContext : DbContext
{
    public GestorInventarioContext()
    {
    }

    public GestorInventarioContext(DbContextOptions<GestorInventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<EntradasInventario> EntradasInventarios { get; set; }

    public virtual DbSet<EstadoInventario> EstadoInventarios { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Producto> Productos { get; set; }

    public virtual DbSet<SalidasInventario> SalidasInventarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EntradasInventario>(entity =>
        {
            entity.HasKey(e => e.IdEntrada).HasName("PK__Entradas__BB164DEA68A74F6A");

            entity.ToTable("EntradasInventario");

            entity.Property(e => e.FechaEntrada)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.EntradasInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__EntradasI__IdPro__29572725");
        });

        modelBuilder.Entity<EstadoInventario>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("EstadoInventario");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.IdMenu).HasName("PK__Menu__C26AF483133AAAF5");

            entity.ToTable("Menu");

            entity.Property(e => e.IdMenu).HasColumnName("idMenu");
            entity.Property(e => e.Icono)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(80)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.IdProducto).HasName("PK__Producto__09889210695D25D2");

            entity.Property(e => e.Descripcion).HasMaxLength(255);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre).HasMaxLength(100);
        });

        modelBuilder.Entity<SalidasInventario>(entity =>
        {
            entity.HasKey(e => e.IdSalida).HasName("PK__SalidasI__5D69EC723F93D039");

            entity.ToTable("SalidasInventario");

            entity.Property(e => e.FechaSalida)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.IdEntradaNavigation).WithMany(p => p.SalidasInventarios)
                .HasForeignKey(d => d.IdEntrada)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalidasIn__IdEnt__2F10007B");

            entity.HasOne(d => d.IdProductoNavigation).WithMany(p => p.SalidasInventarios)
                .HasForeignKey(d => d.IdProducto)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SalidasIn__IdPro__2E1BDC42");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
