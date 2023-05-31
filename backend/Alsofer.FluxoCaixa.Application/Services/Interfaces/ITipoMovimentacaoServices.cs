using Phibra.Prova.Application.Services.Wrappers;
using Phibra.Prova.Domain.Model;

namespace Phibra.Prova.Application.Services.Interfaces
{
    public interface ITipoMovimentacaoServices
    {
        Task<Response<TipoMovimentacaoResponse>> ListarTipoMovimentacao();
      
    }
}
