using ColegioMilitar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ColegioMilitar.Infrastructure.Data;

public class ColegioMilitarContext : DbContext
{
    public DbSet<Cadete>               Cadetes            => Set<Cadete>();
    public DbSet<Supervisor>           Supervisores       => Set<Supervisor>();
    public DbSet<Castigo>              Castigos           => Set<Castigo>();
    public DbSet<Sancion>              Sanciones          => Set<Sancion>();
    public DbSet<BimestreConfig>       BimestresConfig    => Set<BimestreConfig>();
    public DbSet<ActitudMilitarManual> ActitudesMilitares => Set<ActitudMilitarManual>();

    public ColegioMilitarContext(DbContextOptions<ColegioMilitarContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cadete>(e =>
        {
            e.HasKey(c => c.DNI);
            e.Property(c => c.DNI).HasMaxLength(20);
            e.Property(c => c.ApellidosNombres).IsRequired().HasMaxLength(200);
            e.Property(c => c.Division).HasMaxLength(5);
        });

        modelBuilder.Entity<Supervisor>(e =>
        {
            e.HasKey(s => s.DNI);
            e.Property(s => s.DNI).HasMaxLength(20);
            e.Property(s => s.Grado).IsRequired().HasMaxLength(20);
            e.Property(s => s.ApellidosNombres).IsRequired().HasMaxLength(200);
        });

        modelBuilder.Entity<Castigo>(e =>
        {
            e.HasKey(c => c.Codigo);
            e.Property(c => c.Codigo).HasMaxLength(10);
            e.Property(c => c.Descripcion).IsRequired().HasMaxLength(500);
            e.Property(c => c.Nota).HasMaxLength(50);
            e.Property(c => c.PuntosAño3Raw).HasMaxLength(10).HasDefaultValue("0");
            e.Property(c => c.PuntosAño4Raw).HasMaxLength(10).HasDefaultValue("0");
            e.Property(c => c.PuntosAño5Raw).HasMaxLength(10).HasDefaultValue("0");
        });

        modelBuilder.Entity<Sancion>(e =>
        {
            e.HasKey(s => s.Id);
            e.HasOne(s => s.Cadete).WithMany(c => c.Sanciones)
             .HasForeignKey(s => s.CadeteDNI).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(s => s.Supervisor).WithMany(sup => sup.Sanciones)
             .HasForeignKey(s => s.SupervisorDNI).OnDelete(DeleteBehavior.Restrict);
            e.HasOne(s => s.Castigo).WithMany(c => c.Sanciones)
             .HasForeignKey(s => s.CastigoCodigo).OnDelete(DeleteBehavior.Restrict);
            e.HasIndex(s => new { s.CadeteDNI, s.SemanaBimestre });
            e.HasIndex(s => s.Fecha);
        });

        modelBuilder.Entity<BimestreConfig>(e =>
        {
            e.HasKey(b => b.Id);
            e.HasIndex(b => new { b.Bimestre, b.Año, b.NroSemana }).IsUnique();
        });

        modelBuilder.Entity<ActitudMilitarManual>(e =>
        {
            e.HasKey(a => a.Id);
            e.HasOne(a => a.Cadete).WithMany(c => c.ActitudesMilitares)
             .HasForeignKey(a => a.CadeteDNI).OnDelete(DeleteBehavior.Cascade);
            e.HasIndex(a => new { a.CadeteDNI, a.Bimestre, a.AñoAcademico }).IsUnique();
            e.Property(a => a.NotaActitud).HasColumnType("REAL");
        });
    }
}
