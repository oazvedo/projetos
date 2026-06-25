using api.Application.DTOs.Pedido;
using api.Domain.Enums;

namespace api.Application.Services.Interfaces
{
    public interface IPedidoService
    {
        Task<IEnumerable<PedidoDto>> GetAllPedidos();
        Task<IEnumerable<PedidoDto>> GetPedidosByUsuarioId(Guid usuarioId);
        Task<PedidoDto?> GetPedidoById(Guid id);
        Task<PedidoDto> CreatePedido(Guid usuarioId, CreatePedidoRequest request);
        Task<PedidoDto?> UpdatePedido(Guid pedidoId, UpdatePedidoRequest request);
        Task<PedidoDto?> UpdatePedidoStatus(Guid id, PedidoStatus newStatus);
        Task<PedidoDto?> UpdatePedidoContratacao(Guid id, PedidoTipoContratacaoEnum novaContratacao);
        Task<bool> DeleteAsync(Guid id);
    }
}
