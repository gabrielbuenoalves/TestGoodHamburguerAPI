using Microsoft.EntityFrameworkCore;
using TestGoodHamburguer.Data;
using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;
using TestGoodHamburguer.Repositories.Interfaces;

namespace TestGoodHamburguer.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly AppDbContext _context;

        public ItemRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Item?> GetByIdAsync(int id)
        {
            return await _context.Itens.FindAsync(id);
        }

        public async Task<List<Item>> GetAllAsync()
        {
            return await _context.Itens.ToListAsync();
        }

        public async Task<List<Item>> GetByTipoAsync(TipoItem tipo)
        {
            return await _context.Itens
                .Where(i => i.Tipo == tipo)
                .ToListAsync();
        }
    }
}
