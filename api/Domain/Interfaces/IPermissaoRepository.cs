using api.domain;

namespace api.domain.interfaces
{
    public interface IPermissaoRepository
    {
        Task<IEnumerable<Permissao>> GetAllAsync();
        Task<Permissao?> GetByIdAsync(Guid id);
        Task<IEnumerable<Permissao>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<bool> AdicionarAsync(Guid usuarioId, Guid permissaoId);
        Task<bool> RemoverAsync(Guid usuarioId, Guid permissaoId);
        Task<bool> RemoverTodasAsync(Guid usuarioId);
    }
}
