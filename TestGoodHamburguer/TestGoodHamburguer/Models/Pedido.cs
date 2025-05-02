using System.ComponentModel.DataAnnotations;

namespace TestGoodHamburguer.Models
{
    public class Pedido
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Cliente { get; set; } = string.Empty;

        public List<ItemPedido> Itens { get; set; } = new();

        public decimal ValorTotal { get; set; }
    }
}
