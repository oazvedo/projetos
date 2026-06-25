using api.Domain;

namespace api.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetPedidosAsync();
        Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAsync(Guid usuarioId);
        Task<Pedido?> GetPedidoById(Guid id);
        Task<Pedido> AdicionarPedido(Pedido pedido);
        Task<Pedido?> AtualizarPedido(Guid id, Pedido pedido);
        Task<bool> RemoverPedido(Guid id);
    }
}
