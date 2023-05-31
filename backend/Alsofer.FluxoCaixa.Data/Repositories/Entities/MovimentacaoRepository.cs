using Phibra.Prova.Data.Interfaces;
using Phibra.Prova.Domain.Entities;

namespace Phibra.Prova.Data.Repositories.Entities
{
    public class MovimentacaoRepository : EntityBaseRepository<Movimentacao>, IMovimentacaoRepository

    {
        private readonly ApplicationContext _db;
        public MovimentacaoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
