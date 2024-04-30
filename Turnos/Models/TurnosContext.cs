using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Turnos.Models;

namespace Turnos.Models
{
    public class TurnosContext : DbContext
    {
        public TurnosContext(DbContextOptions<TurnosContext> opciones)
        : base(opciones)
        {

        }

        public DbSet<Especialidad> Especialidad { get; set; }

        public DbSet<Paciente> Paciente { get; set; }

        public DbSet<Medico> Medico { get; set; }

        public DbSet<MedicoEspecialidad> MedicoEspecialidad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Especialidad>(entidad =>
            {
                entidad.ToTable("Especialidad");
                entidad.HasKey(e => e.EspecialidadID);
                entidad.Property(e => e.Descripcion)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Paciente>(entidad => 
            {
                entidad.ToTable("Paciente");
                entidad.HasKey(p => p.PacienteID);
                entidad.Property(p => p.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entidad.Property(p => p.Apellido)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entidad.Property(p => p.Direccion)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

                entidad.Property(p => p.Telefono)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
                
                entidad.Property(p => p.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);
            });

            modelBuilder.Entity<Medico>(entidad => 
            {
                entidad.ToTable("Medico");
                entidad.HasKey(m => m.MedicoID);
                entidad.Property(m => m.Nombre)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entidad.Property(m => m.Apellido)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false);

                entidad.Property(m => m.Direccion)
                .IsRequired()
                .HasMaxLength(250)
                .IsUnicode(false);

                entidad.Property(m => m.Telefono)
                .IsRequired()
                .HasMaxLength(20)
                .IsUnicode(false);
                
                entidad.Property(m => m.Email)
                .IsRequired()
                .HasMaxLength(100)
                .IsUnicode(false);

                entidad.Property(m => m.HorarioAtencionDesde)
                .IsRequired()
                .IsUnicode(false);

                entidad.Property(m => m.HorarioAtencionHasta)
                .IsRequired()
                .IsUnicode(false);
            });

            //Definir una restriccion entre la tabla medico y la tabla especialidad, creando una relacion de uno a muchos, un medico puede tener muchas especialidades
            modelBuilder.Entity<MedicoEspecialidad>().HasKey(x => new { x.MedicoID, x.EspecialidadID });

            modelBuilder.Entity<MedicoEspecialidad>().HasOne(x => x.Medico)
            .WithMany(p => p.MedicoEspecialidad)
            .HasForeignKey(p => p.MedicoID);

            modelBuilder.Entity<MedicoEspecialidad>().HasOne(x => x.Especialidad)
            .WithMany(p => p.MedicoEspecialidad)
            .HasForeignKey(p => p.EspecialidadID);
        }
    }
}