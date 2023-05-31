using Phibra.Prova.Application.Services.Interfaces;
using Phibra.Prova.Application.Services.Wrappers;
using Phibra.Prova.Data.Interfaces;
using Phibra.Prova.Domain.Model;

namespace Phibra.Prova.Application.Services
{
    public class TipoMovimentacaoServices : ITipoMovimentacaoServices
    {
        private readonly ITipoMovimentacaoRepository _tipoMovimentacaoRepository;

        public TipoMovimentacaoServices(ITipoMovimentacaoRepository tipoMovimentacaoRepository)
        {
            _tipoMovimentacaoRepository = tipoMovimentacaoRepository;
        }

        public async Task<Response<TipoMovimentacaoResponse>> ListarTipoMovimentacao()
        {
            TipoMovimentacaoResponse tipoMovimentacaoResponse = new TipoMovimentacaoResponse();

            try
            {
                var tipoMovimentacao = await _tipoMovimentacaoRepository.GetAllAsync();

                if (tipoMovimentacao.Any())
                {
                    tipoMovimentacaoResponse.tipoMovimentacao = tipoMovimentacao.ToList();
                    tipoMovimentacaoResponse.Executado = true;
                    tipoMovimentacaoResponse.MensagemRetorno = "Consulta no cadastro do tipo de movimentação efetuada com sucesso";
                }
                else
                {
                    tipoMovimentacaoResponse.Executado = true;
                    tipoMovimentacaoResponse.MensagemRetorno = "Não existem registros para esta consulta";
                }
            }
            catch
            {
                tipoMovimentacaoResponse.Executado = false;
                tipoMovimentacaoResponse.MensagemRetorno = "Erro na cadastro do tipo de movimentação efetuada com sucesso";
            }

            return new Response<TipoMovimentacaoResponse>(tipoMovimentacaoResponse, $"Consulta Tipo Movimentação.");
        }

       
    }
}
