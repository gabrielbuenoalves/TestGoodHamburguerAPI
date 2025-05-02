using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;
using TestGoodHamburguer.Repositories.Interfaces;
using TestGoodHamburguer.Services.Interface;

namespace TestGoodHamburguer.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;

        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> ListarTodosAsync()
        {
            return await _itemRepository.GetAllAsync();
        }

        public async Task<List<Item>> ListarPorTipoAsync(TipoItem tipo)
        {
            return await _itemRepository.GetByTipoAsync(tipo);
        }
    }
}
