

using System.ComponentModel.DataAnnotations;

namespace Phibra.Prova.Domain.Entities
{
    public class SaldoMovimentacao : EntityBase
    {
        [Key]
        public int id { get; set; }


        public decimal saldoDespesa { get; set; }

        public decimal saldoReceita { get; set; }

        public decimal saldoTotal { get; set; }

        public DateTime dt_movimentacao { get; set; }
    }
}
