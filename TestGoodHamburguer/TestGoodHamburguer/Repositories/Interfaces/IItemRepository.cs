using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;

namespace TestGoodHamburguer.Repositories.Interfaces
{
    public interface IItemRepository
    {
        Task<Item?> GetByIdAsync(int id);
        Task<List<Item>> GetAllAsync();
        Task<List<Item>> GetByTipoAsync(TipoItem tipo);
    }
}
