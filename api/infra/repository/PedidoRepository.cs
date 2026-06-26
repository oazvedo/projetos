using api.Domain;
using api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.infra.repository
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly DatabaseContext _context;

        public PedidoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Pedido>> GetPedidosAsync()
        {
            return await _context.Pedidos
                .Include(p => p.Produto)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pedido>> GetPedidosByUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.Pedidos
                .Include(p => p.Produto)
                .Where(p => p.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task<Pedido?> GetPedidoById(Guid id)
        {
            return await _context.Pedidos
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Pedido> AdicionarPedido(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            await _context.SaveChangesAsync();
            await _context.Entry(pedido).Reference(p => p.Produto).LoadAsync();
            return pedido;
        }

        public async Task<Pedido?> AtualizarPedido(Guid id, Pedido pedido)
        {
            var existing = await _context.Pedidos
                .Include(p => p.Produto)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null) return null;

            existing.Status = pedido.Status;
            existing.Contracacao = pedido.Contracacao;
            existing.AtualizadoEm = pedido.AtualizadoEm;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> RemoverPedido(Guid id)
        {
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null) return false;

            _context.Pedidos.Remove(pedido);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
