

using Phibra.Prova.Domain.Entities;

namespace Phibra.Prova.Domain.Model
{
    public class SaldoMovimentacaoResponse
    {
        public SaldoMovimentacao saldoMovimentacao { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}
