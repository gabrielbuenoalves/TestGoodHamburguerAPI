using Microsoft.EntityFrameworkCore;
using TestGoodHamburguer.Data;
using TestGoodHamburguer.Models;
using TestGoodHamburguer.Repositories.Interfaces;

namespace TestGoodHamburguer.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetAllAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(ip => ip.Item)
                .ToListAsync();
        }

        public async Task<Pedido?> GetByIdAsync(int id)
        {
            return await _context.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(ip => ip.Item)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task UpdateAsync(Pedido pedido)
        {
            _context.Pedidos.Update(pedido);
        }

        public async Task DeleteAsync(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
