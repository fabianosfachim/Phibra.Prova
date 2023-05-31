
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Phibra.Prova.Domain.Entities
{
    public class Movimentacao :EntityBase
    {

        [Key]
        public int id { get; set; }

   
        public int id_movimentacao { get; set; }

        public DateTime dt_movimento { get; set; }

        public DateTime dt_lancamento { get; set; }

        public decimal vl_lancamento { get; set; }

    }
}
