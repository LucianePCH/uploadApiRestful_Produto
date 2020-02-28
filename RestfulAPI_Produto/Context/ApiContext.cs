using Microsoft.EntityFrameworkCore;

namespace RestfulAPI_Produto.Models
{
    public partial class ApiContext : DbContext
    {
        public ApiContext()
        {
        }

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TCategoria> TCategoria { get; set; }
        public virtual DbSet<TProduto> TProduto { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TCategoria>(entity =>
            {
                entity.ToTable("TCategoria");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<TProduto>(entity =>
            {
                entity.ToTable("TProduto");

                entity.HasIndex(e => e.IdCategoria)
                    .HasName("idCategoria");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.IdCategoria)
                    .HasColumnName("idCategoria")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.TProduto)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TProduto_ibfk_1");
            });
        }
    }
}
