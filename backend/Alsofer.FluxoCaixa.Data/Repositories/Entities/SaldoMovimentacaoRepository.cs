using Phibra.Prova.Data.Interfaces;
using Phibra.Prova.Domain.Entities;

namespace Phibra.Prova.Data.Repositories.Entities
{
    public class SaldoMovimentacaoRepository : EntityBaseRepository<SaldoMovimentacao>, ISaldoMovimentacaoRepository

    {
        private readonly ApplicationContext _db;
        public SaldoMovimentacaoRepository(ApplicationContext context) : base(context)
        {
            _db = context;
        }
    }
}
