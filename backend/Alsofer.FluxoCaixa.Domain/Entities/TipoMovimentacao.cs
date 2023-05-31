
using System.ComponentModel.DataAnnotations;

namespace Phibra.Prova.Domain.Entities
{
    public class TipoMovimentacao : EntityBase
    {
        [Key]
        public int id_movimentacao { get; set; }

        public string? tp_movimentacao { get; set; }
        public string? desc_movimentacao { get; set; }
    }
}
