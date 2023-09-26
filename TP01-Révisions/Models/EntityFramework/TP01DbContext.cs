using Microsoft.EntityFrameworkCore;

namespace TP01_Révisions.Models.EntityFramework
{
    public partial class TP01DbContext : DbContext
    {
        public TP01DbContext()
        {

        }

        public TP01DbContext(DbContextOptions options) : base(options)
        {

        }

        public virtual DbSet<Produit> Produits { get; set; }
        public virtual DbSet<Marque> Marques { get; set; }
        public virtual DbSet<TypeProduit> TypesProduit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Produit>(entity =>
            {
                entity.HasKey(pdt => pdt.IdProduit).HasName("pk_pdt");

                entity.HasOne(pdtmar => pdtmar.MarquesProduits).WithMany(marpdt => marpdt.ProduitsMarque)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pdt_mar");

                entity.HasOne(pdttpd => pdttpd.TypesProduitProduit).WithMany(tpdpdt => tpdpdt.ProduitsTypesProduit)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_pdt_tpd");
            });

            modelBuilder.Entity<Marque>(entity =>
            {
                entity.HasKey(mar => mar.IdMarque).HasName("pk_mar");
            });

            modelBuilder.Entity<TypeProduit>(entity =>
            {
                entity.HasKey(tpd => tpd.IdTypeProduit).HasName("pk_tpd");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder builder);
    }
}
