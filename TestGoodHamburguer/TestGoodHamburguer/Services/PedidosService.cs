using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;
using TestGoodHamburguer.Repositories;
using TestGoodHamburguer.Repositories.Interfaces;
using TestGoodHamburguer.Services.Interface;

namespace TestGoodHamburguer.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IItemRepository _itemRepository;

        public PedidoService(IPedidoRepository pedidoRepository, IItemRepository itemRepository)
        {
            _pedidoRepository = pedidoRepository;
            _itemRepository = itemRepository;
        }

        public async Task<List<Pedido>> ListarTodosAsync()
        {
            return await _pedidoRepository.GetAllAsync();
        }

        public async Task<Pedido?> BuscarPorIdAsync(int id)
        {
            return await _pedidoRepository.GetByIdAsync(id);
        }

        public async Task CriarAsync(Pedido pedido)
        {
            // 1. Carrega os Itens reais com base no ItemId
            foreach (var itemPedido in pedido.Itens)
            {
                itemPedido.Item = await _itemRepository.GetByIdAsync(itemPedido.ItemId);

                if (itemPedido.Item == null)
                    throw new Exception($"Item com ID {itemPedido.ItemId} não encontrado.");
            }

            // 2. Agora sim você pode validar
            ValidarPedido(pedido);

            // 3. Calcula o valor com desconto
            pedido.ValorTotal = CalcularValorComDesconto(pedido);

            // 4. Persiste
            await _pedidoRepository.AddAsync(pedido);
            await _pedidoRepository.SaveChangesAsync();
        }
        public async Task AtualizarAsync(Pedido pedido)
        {
            foreach (var itemPedido in pedido.Itens)
            {
                itemPedido.Item = await _itemRepository.GetByIdAsync(itemPedido.ItemId);

                if (itemPedido.Item == null)
                    throw new Exception($"Item com ID {itemPedido.ItemId} não encontrado.");
            }

            ValidarPedido(pedido);
            pedido.ValorTotal = CalcularValorComDesconto(pedido);

            await _pedidoRepository.UpdateAsync(pedido);
            await _pedidoRepository.SaveChangesAsync();
        }

        public async Task RemoverAsync(Pedido pedido)
        {
            await _pedidoRepository.DeleteAsync(pedido);
            await _pedidoRepository.SaveChangesAsync();
        }

        // --- Validações e cálculo de desconto ---

        private void ValidarPedido(Pedido pedido)
        {
            var sanduiches = pedido.Itens.Count(i => i.Item.Tipo == TipoItem.Sanduiche);
            var fritas = pedido.Itens.Count(i => i.Item.Nome.ToLower().Contains("frita"));
            var refrigerantes = pedido.Itens.Count(i => i.Item.Nome.ToLower().Contains("refrigerante"));

            if (sanduiches != 1)
                throw new Exception("O pedido deve conter exatamente 1 sanduíche.");

            if (fritas > 1)
                throw new Exception("Só é permitido 1 porção de batata frita por pedido.");

            if (refrigerantes > 1)
                throw new Exception("Só é permitido 1 refrigerante por pedido.");
        }

        private decimal CalcularValorComDesconto(Pedido pedido)
        {
            var total = pedido.Itens.Sum(i => i.Item.Preco);

            var temSanduiche = pedido.Itens.Any(i => i.Item.Tipo == TipoItem.Sanduiche);
            var temFrita = pedido.Itens.Any(i => i.Item.Nome.ToLower().Contains("frita"));
            var temRefrigerante = pedido.Itens.Any(i => i.Item.Nome.ToLower().Contains("refrigerante"));

            if (temSanduiche && temFrita && temRefrigerante)
                return total * 0.80m;
            else if (temSanduiche && temRefrigerante)
                return total * 0.85m;
            else if (temSanduiche && temFrita)
                return total * 0.90m;

            return total;
        }
    }
}
