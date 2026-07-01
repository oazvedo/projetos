using api.Application.DTOs.Common;
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
        public async Task<ActionResult<PagedResult<PedidoDto>>> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var pedidos = await _service.GetAllPedidos(page, pageSize);
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
        public async Task<ActionResult<PagedResult<PedidoDto>>> GetByUsuario(Guid usuarioId, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var pedidos = await _service.GetPedidosByUsuarioId(usuarioId, page, pageSize);
            return Ok(pedidos);
        }

        [HttpGet("meus")]
        [Authorize(Policy = "Pedido.Read")]
        public async Task<ActionResult<PagedResult<PedidoDto>>> GetMeus([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (page < 1) page = 1;
            if (pageSize < 1) pageSize = 10;

            var usuarioId = User.GetId();
            var pedidos = await _service.GetPedidosByUsuarioId(usuarioId, page, pageSize);
            return Ok(pedidos);
        }

        [HttpPost]
        [Authorize(Policy = "Pedido.Create")]
        public async Task<ActionResult<PedidoDto>> Create(CreatePedidoRequest request)
        {
            try
            {
                var usuarioId = User.GetId();
                var pedido = await _service.CreatePedido(usuarioId, request);
                return CreatedAtAction(nameof(GetById), new { id = pedido.Id }, pedido);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "Pedido.UpdateAdmin")]
        public async Task<ActionResult<PedidoDto>> UpdatePedido(Guid id, UpdatePedidoRequest request)
        {
            var pedido = await _service.UpdatePedido(id, request);
            if (pedido == null)
                return NotFound(new {message = "Pedido não encontrado"});
            return NoContent();
        }

        [HttpPatch("{id}/status")]
        [Authorize(Policy = "Pedido.Update")]
        public async Task<ActionResult<PedidoDto>> UpdateStatus(Guid id, UpdateStatusPedidoRequest request)
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

        [HttpPatch("{id}/contratacao")]
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
