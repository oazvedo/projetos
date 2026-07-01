using api.Domain;

namespace api.Domain.Interfaces
{
    public interface IPedidoRepository
    {
        Task<IEnumerable<Pedido>> GetPedidosAsync();
        Task<(IEnumerable<Pedido> Items, int TotalCount)> GetPedidosPagedAsync(int page, int pageSize);
        Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAsync(Guid usuarioId);
        Task<Pedido?> GetPedidoById(Guid id);
        Task<Pedido> AdicionarPedido(Pedido pedido);
        Task<Pedido?> AtualizarPedido(Guid id, Pedido pedido, List<PedidoItem>? newItems = null);
        Task<bool> RemoverPedido(Guid id);
    }
}
