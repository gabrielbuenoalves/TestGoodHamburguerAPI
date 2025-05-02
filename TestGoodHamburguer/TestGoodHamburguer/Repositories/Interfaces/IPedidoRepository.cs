using TestGoodHamburguer.Models;

namespace TestGoodHamburguer.Repositories.Interfaces
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> GetAllAsync();
        Task<Pedido?> GetByIdAsync(int id);
        Task AddAsync(Pedido pedido);
        Task UpdateAsync(Pedido pedido);
        Task DeleteAsync(Pedido pedido);
        Task SaveChangesAsync();
    }
}
