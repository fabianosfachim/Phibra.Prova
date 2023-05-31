using Phibra.Prova.Application.Services.Wrappers;
using Phibra.Prova.Domain.Model;


namespace Phibra.Prova.Application.Services.Interfaces
{
    public interface ISaldoMovimentacaoServices
    {
        Task<Response<SaldoMovimentacaoResponse>> ListarSaldoMovimentacao();
    }
}
