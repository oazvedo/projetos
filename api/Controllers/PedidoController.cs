using api.Application.DTOs.Pedido;
using api.Application.Services.Interfaces;
using api.Application.Utils;
using api.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _service;

        public PedidoController(IPedidoService service)
        {
            _service = service;
        }

        [HttpGet]
        [Authorize(Policy = "Pedido.Read")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetAll()
        {
            var pedidos = await _service.GetAllPedidos();
            return Ok(pedidos);
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "Pedido.Read")]
        public async Task<ActionResult<PedidoDto>> GetById(Guid id)
        {
            var pedido = await _service.GetPedidoById(id);
            if (pedido == null)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return Ok(pedido);
        }

        [HttpGet("usuario/{usuarioId}")]
        [Authorize(Policy = "Pedido.Read")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetByUsuario(Guid usuarioId)
        {
            var pedidos = await _service.GetPedidosByUsuarioId(usuarioId);
            return Ok(pedidos);
        }

        [HttpGet("meus")]
        [Authorize(Policy = "Pedido.Read")]
        public async Task<ActionResult<IEnumerable<PedidoDto>>> GetMeus()
        {
            var usuarioId = User.GetId();
            var pedidos = await _service.GetPedidosByUsuarioId(usuarioId);
            return Ok(pedidos);
        }

        [HttpPost]
        [Authorize(Policy = "Pedido.Create")]
        public async Task<ActionResult<PedidoDto>> Create(CreatePedidoRequest request)
        {
            var usuarioId = User.GetId();
            var pedido = await _service.CreatePedido(usuarioId, request);
            return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Policy = "Pedido.Update")]
        public async Task<ActionResult<PedidoDto>> UpdateStatus(Guid id, UpdatePedidoRequest request)
        {
            try
            {
                var pedido = await _service.UpdatePedidoStatus(id, request.Status);
                if (pedido == null)
                    return NotFound(new { mensagem = "Pedido não encontrado." });

                return Ok(pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Pedido.Update")]
        public async Task<ActionResult<PedidoDto>> Update(Guid id, PutPedidoRequest request)
        {
            try
            {
                var pedido = await _service.UpdatePedidoContratacao(id, request.Contratacao);
                if (pedido == null)
                    return NotFound(new { mensagem = "Pedido não encontrado." });

                return Ok(pedido);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Policy = "Pedido.Delete")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removido = await _service.DeleteAsync(id);
            if (!removido)
                return NotFound(new { mensagem = "Pedido não encontrado." });

            return NoContent();
        }
    }
}
