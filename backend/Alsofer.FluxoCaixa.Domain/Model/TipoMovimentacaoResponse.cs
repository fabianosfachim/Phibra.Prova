using Phibra.Prova.Domain.Entities;

namespace Phibra.Prova.Domain.Model
{
    public class TipoMovimentacaoResponse
    {
        public List<TipoMovimentacao> tipoMovimentacao { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}
