using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP01_Révisions.Models.EntityFramework
{
    [Table("t_e_typeproduit_tpd")]
    public class TypeProduit
    {
        [Key]
        [Column("tpd_id")]
        public int IdTypeProduit { get; set; }

        [Required]
        [Column("tpd_name")]
        public string? NomTypeProduit { get; set; }


        [InverseProperty("TypesProduitProduits")]
        public virtual ICollection<Produit> ProduitsTypesProduit { get; set; } = new List<Produit>();
    }
}
