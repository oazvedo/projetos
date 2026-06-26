using api.application.services;
using api.Application.DTOs.Produto;
using api.Application.Services.Interfaces;
using api.Domain;
using api.Domain.Interfaces;

namespace api.Application.Services
{
    public class ProdutoService : ServiceBase<Produto, ProdutoDto>, IProdutoService
    {
        public ProdutoService(IProdutoRepository repository) : base(repository) { }

        protected override ProdutoDto ToDto(Produto entity) => new()
        {
            Id = entity.Id,
            Nome = entity.Nome,
            Descricao = entity.Descricao,
            Codigo = entity.Codigo,
            Status = entity.Status,
            CriadoEm = entity.CriadoEm,
            AtualizadoEm = entity.AtualizadoEm
        };
    }
}
