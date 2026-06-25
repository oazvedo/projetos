using api.Application.DTOs.Pedido;
using api.Application.Services.Interfaces;
using api.Domain;
using api.Domain.Enums;
using api.Domain.Interfaces;

namespace api.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;

        public PedidoService(IPedidoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PedidoDto>> GetAllPedidos()
        {
            var pedidos = await _repository.GetPedidosAsync();
            return pedidos.Select(ToDto);
        }

        public async Task<IEnumerable<PedidoDto>> GetPedidosByUsuarioId(Guid usuarioId)
        {
            var pedidos = await _repository.GetPedidosByUsuarioIdAsync(usuarioId);
            return pedidos.Select(ToDto);
        }

        public async Task<PedidoDto?> GetPedidoById(Guid id)
        {
            var pedido = await _repository.GetPedidoById(id);
            return pedido == null ? null : ToDto(pedido);
        }

        public async Task<PedidoDto> CreatePedido(Guid usuarioId, CreatePedidoRequest request)
        {
            var pedido = new Pedido(usuarioId, request.contratacao);
            await _repository.AdicionarPedido(pedido);
            return ToDto(pedido);
        }

        public async Task<PedidoDto?> UpdatePedidoStatus(Guid id, PedidoStatus newStatus)
        {
            var pedido = await _repository.GetPedidoById(id);
            if (pedido == null) return null;

            pedido.UpdateStatus(newStatus);
            var updated = await _repository.AtualizarPedido(id, pedido);
            return updated == null ? null : ToDto(updated);
        }

        public async Task<PedidoDto?> UpdatePedidoContratacao(Guid id, PedidoTipoContratacaoEnum novaContratacao)
        {
            var pedido = await _repository.GetPedidoById(id);
            if (pedido == null) return null;

            pedido.UpdateContratacao(novaContratacao);
            var updated = await _repository.AtualizarPedido(id, pedido);
            return updated == null ? null : ToDto(updated);
        }

        public Task<bool> DeleteAsync(Guid id)
            => _repository.RemoverPedido(id);

        private static PedidoDto ToDto(Pedido p) => new()
        {
            Id = p.Id,
            UsuarioId = p.UsuarioId,
            Status = p.Status,
            Contracacao = p.Contracacao,
            CriadoEm = p.CriadoEm,
            AtualizadoEm = p.AtualizadoEm
        };

        public async Task<PedidoDto?> UpdatePedido(Guid pedidoId, UpdatePedidoRequest request)
        {
            var pedido = await _repository.GetPedidoById(pedidoId);
            if (pedido == null) return null;

            pedido.UpdatePedido(request.Contratacao, request.Status);
            var updated = await _repository.AtualizarPedido(pedidoId, pedido);
            return updated == null ? null : ToDto(updated);
        }

    }
}
