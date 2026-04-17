using EShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EShop.Infraestructure.Persistence
{
    public class EShopDbContext(DbContextOptions<EShopDbContext> options) : DbContext(options)
    {
        public DbSet<PersonaEntity> Personas { get; set; }
        public DbSet<SegundoFAEntity> SegundosFA { get; set; }
        public DbSet<UsuarioEntity> Usuarios { get; set; }
        public DbSet<OrigenEntity> Origenes { get; set; }
        public DbSet<SesionEntity> Sesiones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PersonaEntity>(static entity =>
            {
                entity.ToTable("PERSONA");
                entity.HasKey(e => e.IdPersona);

                entity.Property(e => e.IdPersona)
                    .HasColumnName("ID_PERSONA")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Apellidos)
                    .HasColumnName("APELLIDOS")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Correo)
                    .HasColumnName("CORREO")
                    .HasMaxLength(150)
                    .IsRequired();

                entity.Property(e => e.Celular)
                    .HasColumnName("CELULAR")
                    .HasMaxLength(20)
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.FechaActualizacion)
                    .HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v!, DateTimeKind.Utc))
                    .HasColumnName("FECHA_ACTUALIZACION");

                entity.HasIndex(e => e.Correo)
                    .IsUnique();
            });

            modelBuilder.Entity<UsuarioEntity>(entity =>
            {
                entity.ToTable("USUARIO");
                entity.HasKey(e => e.IdUsuario);

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("ID_USUARIO")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Contrasenia)
                    .HasColumnName("CONTRASENIA")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.IdPersona)
                    .HasColumnName("ID_PERSONA")
                    .IsRequired();

                entity.Property(e => e.IdOrigen)
                    .HasColumnName("ID_ORIGEN")
                    .IsRequired();

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .IsRequired();

                entity.Property(e => e.Bloqueo)
                    .HasColumnName("BLOQUEO")
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.FechaActualizacion)
                .HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v!, DateTimeKind.Utc))
                    .HasColumnName("FECHA_ACTUALIZACION");

                entity.Property(e => e.FechaBloqueo)
                    .HasColumnName("FECHA_BLOQUEO");

                entity.HasIndex(e => e.Nombre)
                    .IsUnique();
            });

            modelBuilder.Entity<SegundoFAEntity>(entity =>
            {
                entity.ToTable("SEGUNDO_FA");
                entity.HasKey(e => e.Id2FA);

                entity.Property(e => e.Id2FA)
                    .HasColumnName("ID_SEGUNDO_FA")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("ID_USUARIO")
                    .IsRequired();

                entity.Property(e => e.Contrasenia)
                    .HasColumnName("SECRETO")
                    .HasMaxLength(64)
                    .IsRequired();

                entity.Property(e => e.Activo)
                    .HasColumnName("ACTIVO")
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.FechaActualizacion)
                .HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v!, DateTimeKind.Utc))
                    .HasColumnName("FECHA_ACTUALIZACION");
            });

            modelBuilder.Entity<SesionEntity>(entity =>
            {
                entity.ToTable("SESION");
                entity.HasKey(e => e.IdSesion);

                entity.Property(e => e.IdSesion)
                    .HasColumnName("ID_SESION")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.IdUsuario)
                    .HasColumnName("ID_USUARIO")
                    .IsRequired();

                entity.Property(e => e.Jti)
                    .HasColumnName("JTI")
                    .HasMaxLength(200)
                    .IsRequired();

                entity.Property(e => e.Estado)
                    .HasColumnName("ESTADO")
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.FechaExpiracion)
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    .HasColumnName("FECHA_EXPIRACION");

                entity.Property(e => e.FechaActualizacion)
                    .HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v!, DateTimeKind.Utc))
                    .HasColumnName("FECHA_ACTUALIZACION");

                entity.HasIndex(e => e.Jti)
                    .IsUnique();
            });

            modelBuilder.Entity<OrigenEntity>(entity =>
            {
                entity.ToTable("ORIGEN");
                entity.HasKey(e => e.IdOrigen);
                entity.Property(e => e.IdOrigen)
                    .HasColumnName("ID_ORIGEN")
                    .HasDefaultValueSql("ORIGEN_SEQ.NEXTVAL");

                entity.Property(e => e.Nombre)
                    .HasColumnName("NOMBRE")
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasIndex(e => e.Nombre)
                    .IsUnique();

                entity.Property(e => e.Clave)
                    .HasColumnName("CLAVE")
                    .HasMaxLength(10)
                    .IsRequired();

                entity.Property(e => e.Estatus)
                    .HasColumnName("ESTATUS")
                    .IsRequired();

                entity.Property(e => e.FechaCreacion)
                    .HasColumnName("FECHA_CREACION")
                    .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc))
                    .HasDefaultValueSql("SYSDATE");

                entity.Property(e => e.FechaActualizacion)
                .HasConversion(v => v, v => DateTime.SpecifyKind((DateTime)v!, DateTimeKind.Utc))
                    .HasColumnName("FECHA_ACTUALIZACION");
            });
        }
    }
}
