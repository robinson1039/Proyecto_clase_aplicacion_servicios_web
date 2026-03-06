using Microsoft.EntityFrameworkCore;
using WebApplicationApi.Data.Entities;

namespace WebApplicationApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Red> Red { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Red>(entity =>
            {
                entity.ToTable("red"); // Ajusta el nombre de la tabla según tu BD
                entity.HasKey(e => e.Idr);
                entity.Property(e => e.Idr).HasColumnName("idr");
                entity.Property(e => e.Nombre).HasColumnName("nombre").IsRequired();
                entity.Property(e => e.Url).HasColumnName("url");
                entity.Property(e => e.Pais).HasColumnName("pais");
            });
        }
    }
}