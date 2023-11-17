using R508_TP03_Blazor.Models;

namespace R508_TP03_Blazor.Services
{
    public interface IService
    {
        Task<List<ProduitDto>> GetProduitsAsync(string nomControleur);
        Task<ProduitDetailDto> GetProduitByIdAsync(string nomControleur, int id);
        Task<ProduitDetailDto> GetProduitByStringAsync(string nomControleur, string str);
        /*
        Task<bool> PostProduitAsync(string nomControleur, Produit produit);
        Task<bool> PutProduitAsync(string nomControleur, Produit produit);
        Task<bool> DeleteProduitAsync(string nomControleur, Produit produit);*/

        Task<List<TypeProduitDto>> GetTypeProduitsAsync(string nomControleur);
        Task<List<MarqueDto>> GetMarquesAsync(string nomControleur);

    }
}
