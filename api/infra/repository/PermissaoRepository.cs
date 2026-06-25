using api.domain;
using api.domain.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.infra.repository
{
    public class PermissaoRepository : IPermissaoRepository
    {
        private readonly DatabaseContext _context;

        public PermissaoRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Permissao>> GetAllAsync()
        {
            return await _context.Permissoes.ToListAsync();
        }

        public async Task<Permissao?> GetByIdAsync(Guid id)
        {
            return await _context.Permissoes.FindAsync(id);
        }

        public async Task<IEnumerable<Permissao>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            return await _context.UsuarioPermissoes
                .Where(up => up.UsuarioId == usuarioId)
                .Select(up => up.Permissao)
                .ToListAsync();
        }

        public async Task<bool> AdicionarAsync(Guid usuarioId, Guid permissaoId)
        {
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == usuarioId);
            var permissaoExiste = await _context.Permissoes.AnyAsync(p => p.Id == permissaoId);

            if (!usuarioExiste || !permissaoExiste)
                return false;

            var jaTemPermissao = await _context.UsuarioPermissoes
                .AnyAsync(up => up.UsuarioId == usuarioId && up.PermissaoId == permissaoId);

            if (jaTemPermissao)
                return false;

            _context.UsuarioPermissoes.Add(new UsuarioPermissao
            {
                UsuarioId = usuarioId,
                PermissaoId = permissaoId
            });

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverAsync(Guid usuarioId, Guid permissaoId)
        {
            var usuarioPermissao = await _context.UsuarioPermissoes
                .FirstOrDefaultAsync(up => up.UsuarioId == usuarioId && up.PermissaoId == permissaoId);

            if (usuarioPermissao == null)
                return false;

            _context.UsuarioPermissoes.Remove(usuarioPermissao);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoverTodasAsync(Guid usuarioId)
        {
            var permissoes = await _context.UsuarioPermissoes
                .Where(up => up.UsuarioId == usuarioId)
                .ToListAsync();

            if (!permissoes.Any())
                return false;

            _context.UsuarioPermissoes.RemoveRange(permissoes);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
