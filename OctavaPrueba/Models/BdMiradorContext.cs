﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace OctavaPrueba.Models;

public partial class BdMiradorContext : DbContext
{
    public BdMiradorContext()
    {
    }

    public BdMiradorContext(DbContextOptions<BdMiradorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Abono> Abonos { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<DetalleReservaPaquete> DetalleReservaPaquetes { get; set; }

    public virtual DbSet<DetalleReservaServicio> DetalleReservaServicios { get; set; }

    public virtual DbSet<EstadosReserva> EstadosReservas { get; set; }

    public virtual DbSet<HabitacionMueble> HabitacionMuebles { get; set; }

    public virtual DbSet<Habitacione> Habitaciones { get; set; }

    public virtual DbSet<ImagenAbono> ImagenAbonos { get; set; }

    public virtual DbSet<ImagenHabitacion> ImagenHabitacions { get; set; }

    public virtual DbSet<ImagenPaquete> ImagenPaquetes { get; set; }

    public virtual DbSet<ImagenServicio> ImagenServicios { get; set; }

    public virtual DbSet<Imagene> Imagenes { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Mueble> Muebles { get; set; }

    public virtual DbSet<Paquete> Paquetes { get; set; }

    public virtual DbSet<PaqueteServicio> PaqueteServicios { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<PermisosRole> PermisosRoles { get; set; }

    public virtual DbSet<Reserva> Reservas { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<TipoDocumento> TipoDocumentos { get; set; }

    public virtual DbSet<TipoHabitacione> TipoHabitaciones { get; set; }

    public virtual DbSet<TipoServicio> TipoServicios { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    // Relación entre Reserva y DetalleReservaPaquete
   

    //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
    //        => optionsBuilder.UseSqlServer("Server=SANTIAGO\\SQLEXPRESS;Initial Catalog=BD_Mirador;integrated security=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Abono>(entity =>
        {
            entity.HasKey(e => e.IdAbono).HasName("PK__Abono__A4693DA7ACAE2F13");

            entity.ToTable("Abono");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaAbono)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iva).HasColumnName("IVA");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.Abonos)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Abono__IdReserva__19DFD96B");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.NroDocumento).HasName("PK__Clientes__CC62C91C1295F1B0");

            entity.Property(e => e.NroDocumento)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Clientes__IdRol__02084FDA");

            entity.HasOne(d => d.IdTipoDocumentoNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdTipoDocumento)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Clientes__IdTipo__02FC7413");
        });

        modelBuilder.Entity<DetalleReservaPaquete>(entity =>
        {
            entity.HasKey(e => e.DetalleReservaPaquete1).HasName("PK__DetalleR__2E8BFF25E66B2199");

            entity.ToTable("DetalleReservaPaquete");

            entity.Property(e => e.DetalleReservaPaquete1).HasColumnName("DetalleReservaPaquete");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.DetalleReservaPaquetes)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__IdPaq__14270015");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleReservaPaquetes)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__IdRes__151B244E");
        });

        modelBuilder.Entity<DetalleReservaServicio>(entity =>
        {
            entity.HasKey(e => e.IdDetalleReservaServicio).HasName("PK__DetalleR__D3D91A5A87D36E77");

            entity.ToTable("DetalleReservaServicio");

            entity.HasOne(d => d.IdReservaNavigation).WithMany(p => p.DetalleReservaServicios)
                .HasForeignKey(d => d.IdReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__IdRes__114A936A");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.DetalleReservaServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleRe__IdSer__10566F31");
        });

        modelBuilder.Entity<EstadosReserva>(entity =>
        {
            entity.HasKey(e => e.IdEstadoReserva).HasName("PK__EstadosR__3E654CA800F17936");

            entity.ToTable("EstadosReserva");

            entity.Property(e => e.NombreEstadoReserva)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<HabitacionMueble>(entity =>
        {
            entity.HasKey(e => e.IdHabitacionMueble).HasName("PK__Habitaci__402E049DE25E461D");

            entity.ToTable("HabitacionMueble");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.HabitacionMuebles)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdHab__628FA481");

            entity.HasOne(d => d.IdMuebleNavigation).WithMany(p => p.HabitacionMuebles)
                .HasForeignKey(d => d.IdMueble)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdMue__6383C8BA");
        });

        modelBuilder.Entity<Habitacione>(entity =>
        {
            entity.HasKey(e => e.IdHabitacion).HasName("PK__Habitaci__8BBBF90152428C64");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.IdTipoHabitacionNavigation).WithMany(p => p.Habitaciones)
                .HasForeignKey(d => d.IdTipoHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Habitacio__IdTip__5BE2A6F2");
        });

        modelBuilder.Entity<ImagenAbono>(entity =>
        {
            entity.HasKey(e => e.IdImagenAbono).HasName("PK__ImagenAb__A40FAE53925F394B");

            entity.ToTable("ImagenAbono");

            entity.HasOne(d => d.IdAbonoNavigation).WithMany(p => p.ImagenAbonos)
                .HasForeignKey(d => d.IdAbono)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenAbo__IdAbo__1DB06A4F");

            entity.HasOne(d => d.IdImagenNavigation).WithMany(p => p.ImagenAbonos)
                .HasForeignKey(d => d.IdImagen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenAbo__IdIma__1CBC4616");
        });

        modelBuilder.Entity<ImagenHabitacion>(entity =>
        {
            entity.HasKey(e => e.IdImagenHabi).HasName("PK__ImagenHa__5B5FF6AD8470DF56");

            entity.ToTable("ImagenHabitacion");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.ImagenHabitacions)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenHab__IdHab__6754599E");

            entity.HasOne(d => d.IdImagenNavigation).WithMany(p => p.ImagenHabitacions)
                .HasForeignKey(d => d.IdImagen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenHab__IdIma__66603565");
        });

        modelBuilder.Entity<ImagenPaquete>(entity =>
        {
            entity.HasKey(e => e.IdImagenPaque).HasName("PK__ImagenPa__AD53DF94C1FC3BEB");

            entity.ToTable("ImagenPaquete");

            entity.HasOne(d => d.IdImagenNavigation).WithMany(p => p.ImagenPaquetes)
                .HasForeignKey(d => d.IdImagen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenPaq__IdIma__778AC167");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.ImagenPaquetes)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenPaq__IdPaq__787EE5A0");
        });

        modelBuilder.Entity<ImagenServicio>(entity =>
        {
            entity.HasKey(e => e.IdImagenServi).HasName("PK__ImagenSe__3C03784C13A0C448");

            entity.ToTable("ImagenServicio");

            entity.HasOne(d => d.IdImagenNavigation).WithMany(p => p.ImagenServicios)
                .HasForeignKey(d => d.IdImagen)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenSer__IdIma__6FE99F9F");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.ImagenServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ImagenSer__IdSer__70DDC3D8");
        });

        modelBuilder.Entity<Imagene>(entity =>
        {
            entity.HasKey(e => e.IdImagen).HasName("PK__Imagenes__B42D8F2AE345C7C8");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__MetodoPa__6F49A9BE655372DA");

            entity.ToTable("MetodoPago");

            entity.Property(e => e.NomMetodoPago)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Mueble>(entity =>
        {
            entity.HasKey(e => e.IdMueble).HasName("PK__Muebles__829D15E897CA2D58");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.FechaRegistro)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Valor).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<Paquete>(entity =>
        {
            entity.HasKey(e => e.IdPaquete).HasName("PK__Paquetes__DE278F8B4B19D424");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NomPaquete)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdHabitacionNavigation).WithMany(p => p.Paquetes)
                .HasForeignKey(d => d.IdHabitacion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Paquetes__IdHabi__74AE54BC");
        });

        modelBuilder.Entity<PaqueteServicio>(entity =>
        {
            entity.HasKey(e => e.IdPaqueteServicio).HasName("PK__PaqueteS__DE5C2BC269981C11");

            entity.ToTable("PaqueteServicio");

            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdPaqueteNavigation).WithMany(p => p.PaqueteServicios)
                .HasForeignKey(d => d.IdPaquete)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaqueteSe__IdPaq__7B5B524B");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.PaqueteServicios)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PaqueteSe__IdSer__7C4F7684");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.IdPermiso).HasName("PK__Permisos__0D626EC8EE2AA9B9");

            entity.Property(e => e.NomPermiso)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PermisosRole>(entity =>
        {
            entity.HasKey(e => e.IdPermisosRoles).HasName("PK__Permisos__8C257ED946E43BA0");

            entity.HasOne(d => d.IdPermisoNavigation).WithMany(p => p.PermisosRoles)
                .HasForeignKey(d => d.IdPermiso)
                .HasConstraintName("FK__PermisosR__IdPer__4E88ABD4");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.PermisosRoles)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__PermisosR__IdRol__4D94879B");
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.HasKey(e => e.IdReserva).HasName("PK__Reservas__0E49C69DAAB54F8D");

            entity.Property(e => e.FechaFinalizacion).HasColumnType("datetime");
            entity.Property(e => e.FechaInicio).HasColumnType("datetime");
            entity.Property(e => e.FechaReserva)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Iva).HasColumnName("IVA");
            entity.Property(e => e.NroDocumentoCliente)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdEstadoReservaNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.IdEstadoReserva)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__IdEsta__0C85DE4D");

            entity.HasOne(d => d.MetodoPagoNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.MetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__Metodo__0D7A0286");

            entity.HasOne(d => d.NroDocumentoClienteNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.NroDocumentoCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__NroDoc__0A9D95DB");

            entity.HasOne(d => d.NroDocumentoUsuarioNavigation).WithMany(p => p.Reservas)
                .HasForeignKey(d => d.NroDocumentoUsuario)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Reservas__NroDoc__0B91BA14");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRol).HasName("PK__Roles__2A49584CF41CEB44");

            entity.Property(e => e.NomRol)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio).HasName("PK__Servicio__2DCCF9A2E133ADCB");

            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NomServicio)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Precio).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.IdTipoServicioNavigation).WithMany(p => p.Servicios)
                .HasForeignKey(d => d.IdTipoServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Servicios__IdTip__6D0D32F4");
        });

        modelBuilder.Entity<TipoDocumento>(entity =>
        {
            entity.HasKey(e => e.IdTipoDocumento).HasName("PK__TipoDocu__3AB3332FC9A90625");

            entity.ToTable("TipoDocumento");

            entity.Property(e => e.NomTipoDcumento)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoHabitacione>(entity =>
        {
            entity.HasKey(e => e.IdTipoHabitacion).HasName("PK__TipoHabi__AB75E87C343675F0");

            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.NomTipoHabitacion)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TipoServicio>(entity =>
        {
            entity.HasKey(e => e.IdTipoServicio).HasName("PK__TipoServ__E29B3EA7B64AE76B");

            entity.Property(e => e.NombreTipoServicio)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.NroDocumento).HasName("PK__Usuarios__CC62C91CF0D2702F");

            entity.Property(e => e.NroDocumento).ValueGeneratedNever();
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Celular)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Contrasena)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Estado).HasDefaultValue(true);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__Usuarios__IdRol__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);

        // Configurar clave primaria compuesta para DetalleReservaPaquete
        modelBuilder.Entity<DetalleReservaPaquete>()
            .HasKey(dp => new { dp.IdReserva, dp.IdPaquete });

        modelBuilder.Entity<DetalleReservaPaquete>()
            .HasOne(dp => dp.IdReservaNavigation)
            .WithMany(r => r.DetalleReservaPaquetes)
            .HasForeignKey(dp => dp.IdReserva);

        modelBuilder.Entity<DetalleReservaPaquete>()
            .HasOne(dp => dp.IdPaqueteNavigation)
            .WithMany()
            .HasForeignKey(dp => dp.IdPaquete);

        // Configurar clave primaria compuesta para DetalleReservaServicio
        modelBuilder.Entity<DetalleReservaServicio>()
            .HasKey(ds => new { ds.IdReserva, ds.IdServicio });

        modelBuilder.Entity<DetalleReservaServicio>()
            .HasOne(ds => ds.IdReservaNavigation)
            .WithMany(r => r.DetalleReservaServicios)
            .HasForeignKey(ds => ds.IdReserva);

        modelBuilder.Entity<DetalleReservaServicio>()
            .HasOne(ds => ds.IdServicioNavigation)
            .WithMany()
            .HasForeignKey(ds => ds.IdServicio);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
