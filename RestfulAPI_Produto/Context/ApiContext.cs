using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

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

        public virtual DbSet<Tcategoria> Tcategoria { get; set; }
        public virtual DbSet<Tproduto> Tproduto { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tcategoria>(entity =>
            {
                entity.ToTable("tcategoria");

                entity.Property(e => e.Id).HasColumnType("int(11)");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(80)");
            });

            modelBuilder.Entity<Tproduto>(entity =>
            {
                entity.ToTable("tproduto");

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
                    .WithMany(p => p.Tproduto)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("tproduto_ibfk_1");
            });
        }
    }
}
