using Phibra.Prova.Application.Services.Wrappers;
using Phibra.Prova.Domain.Model;

namespace Phibra.Prova.Application.Services.Interfaces
{
    public interface IMovimentacaoServices
    {
        Task<Response<MovimentacaoResponse>> ListarMovimentacao();
        Task<Response<MovimentacaoResponse>> ListarMovimentacao(int id);
        Task<Response<MovimentacaoResponse>> AdicionarMovimentacao(MovimentacaoRequest movimentacaoRequest);
        Task<Response<MovimentacaoResponse>> AtualizarMovimentacao(MovimentacaoRequest movimentacaoRequest);
        Task<Response<MovimentacaoResponse>> ExcluirMovimentacao(int id);
    }
}
