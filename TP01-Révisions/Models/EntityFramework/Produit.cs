using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP01_Révisions.Models.EntityFramework
{
    [Table("t_e_produit_pdt")]
    public class Produit
    {
        [Key]
        [Column("pdt_id")]
        public int IdProduit { get; set; }

        [Required]
        [Column("pdt_name")]
        public string? NomProduit { get; set; }

        [Column("pdt_description")]
        public string? Description { get; set; }

        [Column("pdt_photo_name")]
        public string? NomPhoto { get; set; }

        [Column("pdt_photo_uri")]
        public string? UriPhoto { get; set; }

        [Required]
        [Column("pdt_tpd_id")]
        public int IdType { get; set; }

        [Required]
        [Column("pdt_mar_id")]
        public int IdMarque { get; set; }

        [Column("pdt_stock_reel")]
        public int StockReel { get; set; }

        [Column("pdt_stock_min")]
        public int StockMin { get; set; }

        [Column("pdt_stock_max")]
        public int StockMax { get; set; }

        [ForeignKey(nameof(IdMarque))]
        [InverseProperty("ProduitsMarque")]
        public virtual Marque MarquesProduits { get; set; } = null!;

        [ForeignKey(nameof(IdType))]
        [InverseProperty("ProduitsTypesProduit")]
        public virtual TypeProduit TypesProduitsProduit { get; set; } = null!;
    }
}
