using AutoMapper;
using TestGoodHamburguer.DTOs;
using TestGoodHamburguer.Models;

public class PedidoProfile : Profile
{
    public PedidoProfile()
    {
        // Entrada (DTO → Entidade)
        CreateMap<CreatePedidoDto, Pedido>();

        CreateMap<ItemPedidoDto, ItemPedido>()
            .ForMember(dest => dest.Item, opt => opt.Ignore()); // O Item será carregado manualmente pelo ItemId

        // Saída (Entidade → DTO)
        CreateMap<Pedido, PedidoDto>();

        CreateMap<ItemPedido, ItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Item.Id))
            .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Item.Nome))
            .ForMember(dest => dest.Preco, opt => opt.MapFrom(src => src.Item.Preco))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Item.Tipo.ToString()));
    }
}
