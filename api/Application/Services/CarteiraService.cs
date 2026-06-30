using api.application.services;
using api.Application.DTOs.Carteira;
using api.Application.Services.Interfaces;
using api.Domain;
using api.Domain.Interfaces;

namespace api.Application.Services
{
    public class CarteiraService : ServiceBase<Carteira, CarteiraDto>, ICarteiraService
    {
        public CarteiraService(ICarteiraRepository repository) : base(repository) { }

        public async Task<CarteiraDto> UpdateCarteira(Guid id, UpdateCarteiraRequest request)
        {
            var carteira = await _repository.GetByIdAsync(id)
                ?? throw new KeyNotFoundException($"Carteira {id} não encontrada");

            carteira.UpdateBalance(request.Saldo);

            var updated = await _repository.UpdateAsync(carteira);
            return ToDto(updated!);
        }

        protected override CarteiraDto ToDto(Carteira entity) => new()
        {
            Id = entity.Id,
            UsuarioId = entity.UsuarioId,
            UsuarioNome = entity.Usuario.Nome,
            Saldo = entity.Saldo,
            CriadoEm = entity.CriadoEm,
            AtualizadoEm = entity.AtualizadoEm
        };

    }
}