using Phibra.Prova.Domain.Entities;

namespace Phibra.Prova.Domain.Model
{
    public class MovimentacaoResponse
    {
        public List<Movimentacao> movimentacao { get; set; }
        public Movimentacao objmovimentacao { get; set; }
        public bool Executado { get; set; }
        public string MensagemRetorno { get; set; }
    }
}
