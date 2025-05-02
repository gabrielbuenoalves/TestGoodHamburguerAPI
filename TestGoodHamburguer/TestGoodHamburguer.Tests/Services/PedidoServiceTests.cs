using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;
using TestGoodHamburguer.Repositories.Interfaces;
using TestGoodHamburguer.Services;

namespace TestGoodHamburguer.Tests.Services
{
    public class PedidoServiceTests
    {
        private readonly PedidoService _service;
        private readonly Mock<IPedidoRepository> _pedidoRepoMock;
        private readonly Mock<IItemRepository> _itemRepoMock;

        public PedidoServiceTests()
        {
            _pedidoRepoMock = new Mock<IPedidoRepository>();
            _itemRepoMock = new Mock<IItemRepository>();

            _pedidoRepoMock.Setup(r => r.AddAsync(It.IsAny<Pedido>())).Returns(Task.CompletedTask);
            _pedidoRepoMock.Setup(r => r.SaveChangesAsync()).Returns(Task.CompletedTask);
            _pedidoRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Pedido>())).Returns(Task.CompletedTask);
            _pedidoRepoMock.Setup(r => r.DeleteAsync(It.IsAny<Pedido>())).Returns(Task.CompletedTask);

            _itemRepoMock.Setup(repo => repo.GetByIdAsync(It.IsAny<int>())).ReturnsAsync((int id) => new Item
            {
                Id = id,
                Nome = id switch
                {
                    1 => "X-Burger",
                    4 => "Frita",
                    5 => "Refrigerante",
                    _ => "Desconhecido"
                },
                Preco = id switch
                {
                    1 => 5m,
                    4 => 2m,
                    5 => 2.5m,
                    _ => 0m
                },
                Tipo = id switch
                {
                    1 => TipoItem.Sanduiche,
                    4 or 5 => TipoItem.Extra,
                    _ => TipoItem.Extra
                }
            });

            _service = new PedidoService(_pedidoRepoMock.Object, _itemRepoMock.Object);
        }

        [Fact]
        public async Task DeveAplicarDescontoDe20PorCento()
        {
            var pedido = CriarPedidoComItemIds(new[] { 1, 4, 5 });
            await _service.CriarAsync(pedido);
            Assert.Equal(7.60m, pedido.ValorTotal);
        }

        [Fact]
        public async Task DeveAplicarDescontoDe15PorCento()
        {
            var pedido = CriarPedidoComItemIds(new[] { 1, 5 });
            await _service.CriarAsync(pedido);
            Assert.Equal(6.375m, pedido.ValorTotal);
        }

        [Fact]
        public async Task DeveAplicarDescontoDe10PorCento()
        {
            var pedido = CriarPedidoComItemIds(new[] { 1, 4 });
            await _service.CriarAsync(pedido);
            Assert.Equal(6.30m, pedido.ValorTotal);
        }

        [Fact]
        public async Task NaoDeveAplicarDesconto()
        {
            var pedido = CriarPedidoComItemIds(new[] { 1 });
            await _service.CriarAsync(pedido);
            Assert.Equal(5.00m, pedido.ValorTotal);
        }

        [Fact]
        public async Task DeveAtualizarPedidoComNovoItem()
        {
            var pedido = CriarPedidoComItemIds(new[] { 1, 4 });
            await _service.CriarAsync(pedido);

            pedido.Itens.Add(new ItemPedido { ItemId = 5 });
            await _service.AtualizarAsync(pedido);

            Assert.Equal(7.60m, pedido.ValorTotal);
        }

        [Fact]
        public async Task DeveRemoverPedido()
        {
            var pedido = CriarPedidoComItemIds(new[] { 1 });
            await _service.RemoverAsync(pedido);

            _pedidoRepoMock.Verify(r => r.DeleteAsync(pedido), Times.Once);
            _pedidoRepoMock.Verify(r => r.SaveChangesAsync(), Times.Once);
        }

        private Pedido CriarPedidoComItemIds(int[] itemIds)
        {
            return new Pedido
            {
                Cliente = "Gabriel",
                Itens = itemIds.Select(id => new ItemPedido { ItemId = id }).ToList()
            };
        }
    }

}
