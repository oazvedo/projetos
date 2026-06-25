using api.Application.DTOs.Permissao;

namespace api.application.services.interfaces
{
    public interface IPermissaoService
    {
        Task<IEnumerable<PermissaoDto>> GetAllAsync();
        Task<IEnumerable<PermissaoDto>> GetByUsuarioIdAsync(Guid usuarioId);
        Task<bool> AdicionarAsync(Guid usuarioId, Guid permissaoId);
        Task<bool> RemoverAsync(Guid usuarioId, Guid permissaoId);
        Task<bool> RemoverTodasPermissoesAsync(Guid usuarioId);
    }
}
