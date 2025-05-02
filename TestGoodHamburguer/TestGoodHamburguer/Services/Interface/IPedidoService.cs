using TestGoodHamburguer.Models;

namespace TestGoodHamburguer.Services.Interface
{
    public interface IPedidoService
    {
        Task<List<Pedido>> ListarTodosAsync();
        Task<Pedido?> BuscarPorIdAsync(int id);
        Task CriarAsync(Pedido pedido);
        Task AtualizarAsync(Pedido pedido);
        Task RemoverAsync(Pedido pedido);
    }
}
