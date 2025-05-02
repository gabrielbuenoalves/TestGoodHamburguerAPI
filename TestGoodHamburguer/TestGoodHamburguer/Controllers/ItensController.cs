
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestGoodHamburguer.Data;
using TestGoodHamburguer.DTOs;
using TestGoodHamburguer.Enums;
using TestGoodHamburguer.Models;
using TestGoodHamburguer.Services.Interface;

namespace TestGoodHamburguer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItensController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItensController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<ItemDto>>> GetAll()
        {
            var itens = await _itemService.ListarTodosAsync();
            var result = _mapper.Map<List<ItemDto>>(itens);
            return Ok(result);
        }

        [HttpGet("sanduiches")]
        public async Task<ActionResult<List<ItemDto>>> GetSanduiches()
        {
            var itens = await _itemService.ListarPorTipoAsync(TipoItem.Sanduiche);
            var result = _mapper.Map<List<ItemDto>>(itens);
            return Ok(result);
        }

        [HttpGet("extras")]
        public async Task<ActionResult<List<ItemDto>>> GetExtras()
        {
            var itens = await _itemService.ListarPorTipoAsync(TipoItem.Extra);
            var result = _mapper.Map<List<ItemDto>>(itens);
            return Ok(result);
        }
    }
}
