using R508_TP03_Blazor.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace R508_TP03_Blazor.Services
{
    public class WSService : IService
    {
        public readonly HttpClient httpClient;

        public WSService(string url)
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<ProduitDto>> GetProduitsAsync(string nomControleur)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<ProduitDto>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProduitDetailDto> GetProduitByIdAsync(string nomControleur, int id)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<ProduitDetailDto>(string.Concat(nomControleur, "/", id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<ProduitDetailDto> GetProduitByStringAsync(string nomControleur, string str)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<ProduitDetailDto>(string.Concat(nomControleur, "/", str));
            }
            catch (Exception)
            {
                return null;
            }
        }

        /*public async Task<bool> PostProduitAsync(string nomControleur, Produit produit)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<Produit>(nomControleur, produit);
                return response.IsSuccessStatusCode;

            }
            catch (Exception)
            {
                return false;
            }
        }
        
        public async Task<bool> PutProduitAsync(string nomControleur, Produit produit)
        {
            try
            {
                HttpResponseMessage response = await httpClient.PutAsJsonAsync(string.Concat(nomControleur, "/", produit.ProduitId), produit);
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteProduitAsync(string nomControleur, Produit produit)
        {
            try
            {
                HttpResponseMessage response = await httpClient.DeleteAsync(string.Concat(nomControleur, "/", produit.ProduitId));
                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }*/
        public async Task<List<TypeProduitDto>> GetTypeProduitsAsync(string nomControleur)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<TypeProduitDto>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<List<MarqueDto>> GetMarquesAsync(string nomControleur)
        {
            try
            {
                return await httpClient.GetFromJsonAsync<List<MarqueDto>>(nomControleur);
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
