using AutoMapper;
using TestGoodHamburguer.DTOs;
using TestGoodHamburguer.Models;

namespace TestGoodHamburguer.Mappings
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemDto>()
                .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));
        }
    }
}
