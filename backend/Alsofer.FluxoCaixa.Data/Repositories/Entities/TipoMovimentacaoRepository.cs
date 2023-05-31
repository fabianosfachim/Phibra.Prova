using Phibra.Prova.Data.Interfaces;
using Phibra.Prova.Domain.Entities;

namespace Phibra.Prova.Data.Repositories.Entities
{
    public class TipoMovimentacaoRepository : EntityBaseRepository<TipoMovimentacao>, ITipoMovimentacaoRepository
    {
        private readonly ApplicationContext _db;
        public TipoMovimentacaoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }



}
