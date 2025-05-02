using Swashbuckle.AspNetCore.Filters;
using TestGoodHamburguer.DTOs;

namespace TestGoodHamburguer
{
    public class CreatePedidoDtoExample : IExamplesProvider<CreatePedidoDto>
    {
        public CreatePedidoDto GetExamples()
        {
            return new CreatePedidoDto
            {
                Cliente = "Gabriel",
                Itens = new List<ItemPedidoDto>
                {
                    new ItemPedidoDto { ItemId = 3 },
                    new ItemPedidoDto { ItemId = 4 },
                    new ItemPedidoDto { ItemId = 5 }
                }
            };
        }
    }

}
