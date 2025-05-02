using System.ComponentModel.DataAnnotations;


namespace TestGoodHamburguer.DTOs
{
    public class CreatePedidoDto
    {
        [Required]
        public string Cliente { get; set; }

        [Required]
        public List<ItemPedidoDto> Itens { get; set; }
    }
}
