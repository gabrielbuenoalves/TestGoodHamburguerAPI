using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;

namespace TestGoodHamburguer.Services.Interface
{
    public interface IItemService
    {
        Task<List<Item>> ListarTodosAsync();
        Task<List<Item>> ListarPorTipoAsync(TipoItem tipo);
    }
}
