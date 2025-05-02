using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;

namespace TestGoodHamburguer.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Itens.Any()) return;

            var itens = new List<Item>
            {
                new() { Nome = "X-Burger", Preco = 5.00m, Tipo = TipoItem.Sanduiche },
                new() { Nome = "X-Egg", Preco = 4.50m, Tipo = TipoItem.Sanduiche },
                new() { Nome = "X-Bacon", Preco = 7.00m, Tipo = TipoItem.Sanduiche },
                new() { Nome = "Batata frita", Preco = 2.00m, Tipo = TipoItem.Extra },
                new() { Nome = "Refrigerante", Preco = 2.50m, Tipo = TipoItem.Extra }
            };

            context.Itens.AddRange(itens);
            context.SaveChanges();
        }
    }
}
