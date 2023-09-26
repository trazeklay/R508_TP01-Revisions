using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TP01_Révisions.Models.EntityFramework
{
    [Table("t_e_marque_mar")]
    public class Marque
    {
        [Key]
        [Column("mar_id")]
        public int IdMarque { get; set; }

        [Required]
        [Column("mar_name")]
        public string? NomMarque { get; set; }

        [InverseProperty("MarquesProduits")]
        public virtual ICollection<Produit> ProduitsMarque { get; set; } = new List<Produit>();
    }
}
