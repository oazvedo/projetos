using api.domain.interfaces;

namespace api.Domain.Interfaces
{
    public interface ICarteiraRepository : IRepositoryBase<Carteira>
    {
        Task <Carteira?> GetCarteiraByUsuarioId(Guid id);
    }
}