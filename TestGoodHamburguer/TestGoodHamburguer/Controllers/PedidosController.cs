using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using TestGoodHamburguer.DTOs;
using TestGoodHamburguer.Models;
using TestGoodHamburguer.Services.Interface;

namespace TestGoodHamburguer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;
        private readonly IMapper _mapper;

        public PedidosController(IPedidoService pedidoService, IMapper mapper)
        {
            _pedidoService = pedidoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<PedidoDto>>> GetAll()
        {
            var pedidos = await _pedidoService.ListarTodosAsync();
            var result = _mapper.Map<List<PedidoDto>>(pedidos);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PedidoDto>> GetById(int id)
        {
            var pedido = await _pedidoService.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound("Pedido não encontrado");

            var result = _mapper.Map<PedidoDto>(pedido);
            return Ok(result);
        }

        [HttpPost]
        [SwaggerRequestExample(typeof(CreatePedidoDto), typeof(CreatePedidoDtoExample))]
        public async Task<ActionResult> Create([FromBody] CreatePedidoDto dto)
        {
            try
            {
                var pedido = _mapper.Map<Pedido>(dto);
                await _pedidoService.CriarAsync(pedido);
                return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, _mapper.Map<PedidoDto>(pedido));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] CreatePedidoDto dto)
        {
            var pedidoExistente = await _pedidoService.BuscarPorIdAsync(id);
            if (pedidoExistente == null)
                return NotFound("Pedido não encontrado");

            try
            {
                pedidoExistente.Cliente = dto.Cliente;
                pedidoExistente.Itens = _mapper.Map<List<ItemPedido>>(dto.Itens);
                await _pedidoService.AtualizarAsync(pedidoExistente);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var pedido = await _pedidoService.BuscarPorIdAsync(id);
            if (pedido == null)
                return NotFound("Pedido não encontrado.");

            await _pedidoService.RemoverAsync(pedido);
            return NoContent();
        }
    }

}
