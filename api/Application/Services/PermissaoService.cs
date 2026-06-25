using api.Application.DTOs.Permissao;
using api.application.services.interfaces;
using api.domain.interfaces;
using api.domain;

namespace api.application.services
{
    public class PermissaoService : IPermissaoService
    {
        private readonly IPermissaoRepository _repository;

        public PermissaoService(IPermissaoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<PermissaoDto>> GetAllAsync()
        {
            var permissoes = await _repository.GetAllAsync();
            return permissoes.Select(p => new PermissaoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao
            });
        }

        public async Task<IEnumerable<PermissaoDto>> GetByUsuarioIdAsync(Guid usuarioId)
        {
            var permissoes = await _repository.GetByUsuarioIdAsync(usuarioId);
            return permissoes.Select(p => new PermissaoDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao
            });
        }

        public Task<bool> RemoverTodasPermissoesAsync(Guid usuarioId)
            => _repository.RemoverTodasAsync(usuarioId);

        public Task<bool> AdicionarAsync(Guid usuarioId, Guid permissaoId)
            => _repository.AdicionarAsync(usuarioId, permissaoId);

        public Task<bool> RemoverAsync(Guid usuarioId, Guid permissaoId)
            => _repository.RemoverAsync(usuarioId, permissaoId);
    }
}
