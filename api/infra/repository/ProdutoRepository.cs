using api.Domain;
using api.Domain.Interfaces;

namespace api.infra.repository
{
    public class ProdutoRepository : RepositoryBase<Produto>, IProdutoRepository
    {
        public ProdutoRepository(DatabaseContext context) : base(context) {}
    }
}