namespace TestGoodHamburguer.DTOs
{
    public class PedidoDto
    {
        public int Id { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public decimal ValorTotal { get; set; }
        public List<ItemDto> Itens { get; set; } = new();
    }
}
