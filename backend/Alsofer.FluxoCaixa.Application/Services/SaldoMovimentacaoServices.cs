using Phibra.Prova.Application.Services.Interfaces;
using Phibra.Prova.Application.Services.Wrappers;
using Phibra.Prova.Data.Repositories.Entities;
using Phibra.Prova.Domain.Entities;
using Phibra.Prova.Domain.Model;

namespace Phibra.Prova.Application.Services
{
    public class SaldoMovimentacaoServices : ISaldoMovimentacaoServices
    {
        private readonly SaldoMovimentacaoRepository _saldoMovimentacaoRepository;
        private readonly MovimentacaoServices _movimentacaoServices;
        private const int Receita = 1;
        private const int Despesa = 2;

        public SaldoMovimentacaoServices(SaldoMovimentacaoRepository saldoMovimentacaoRepository, 
                                         MovimentacaoServices movimentacaoServices)
        {
            _saldoMovimentacaoRepository = saldoMovimentacaoRepository;
            _movimentacaoServices = movimentacaoServices;
        }

        public async Task<Response<SaldoMovimentacaoResponse>> ListarSaldoMovimentacao()
        {
            SaldoMovimentacaoResponse saldoMovimentacaoResponse = new SaldoMovimentacaoResponse();

            try
            {
                var listaTotalSaldoMovimentacao = await TotalSaldoMovimentacaoAsync();

                if (listaTotalSaldoMovimentacao != null)
                {
                    saldoMovimentacaoResponse.saldoMovimentacao = listaTotalSaldoMovimentacao;
                    saldoMovimentacaoResponse.Executado = true;
                    saldoMovimentacaoResponse.MensagemRetorno = "Consulta de saldo movimentação efetuada com sucesso";
                }
                else
                {
                    saldoMovimentacaoResponse.Executado = true;
                    saldoMovimentacaoResponse.MensagemRetorno = "Não existem saldo movimentação cadastrados no banco de dados";
                }
            }
            catch
            {
                saldoMovimentacaoResponse.Executado = false;
                saldoMovimentacaoResponse.MensagemRetorno = "Erro na consulta de saldo de movimentação";
            }

            return new Response<SaldoMovimentacaoResponse>(saldoMovimentacaoResponse, $"Lista movimentação.");
        }

    
        private async Task<List<Movimentacao>> ListarMovimentacaoAsync()
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();
            List<Movimentacao> movimentacao = new List<Movimentacao>();

            try
            {
                var listaMovimentacao = await _movimentacaoServices.ListarMovimentacao();

                if (listaMovimentacao.Data.movimentacao.Any())
                {
                    movimentacao = listaMovimentacao.Data.movimentacao;
                }

            }
            catch
            {
                return null;
            }

            return movimentacao;
        }

        private async Task<SaldoMovimentacao> SaldoMovimentacaoAsync()
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();
            SaldoMovimentacao saldoMovimentacao = new SaldoMovimentacao();

            DateTime dataConsulta = DateTime.Now;

            try
            {
                var saldo = await _saldoMovimentacaoRepository.GetAsync(x => x.dt_movimentacao >= DateTime.Parse(dataConsulta.ToString("yyyy-MM-dd HH:mm:ss")) && x.dt_movimentacao <= DateTime.Parse(dataConsulta.AddSeconds(-330).ToString("yyyy-MM-dd HH:mm:ss")));

                if (saldo.Any())
                {
                    foreach(var item in saldo) 
                    {
                        saldoMovimentacao = item;
                    }
                }

            }
            catch
            {
                return null;
            }

            return saldoMovimentacao;
        }

        private async void AdicionarSaldoMovimentacaoAsync(SaldoMovimentacaoRequest saldoMovimentacaoRequest)
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();
            SaldoMovimentacao saldoMovimentacao = new SaldoMovimentacao();
            int id = int.MinValue;

            try
            {
                await _saldoMovimentacaoRepository.AddAsync(saldoMovimentacaoRequest.saldoMovimentacao);
                id = saldoMovimentacaoRequest.saldoMovimentacao.id;
            }
            catch
            {
                throw new Exception("Erro ao inserir o saldo movimentação");
            }
        }

        private async Task<SaldoMovimentacao> TotalSaldoMovimentacaoAsync()
        {
            SaldoMovimentacao saldoMovimentacao = new SaldoMovimentacao();
            decimal saldoDespesa = 0;
            decimal saldoReceita = 0;
            decimal saldoTotal = 0;
            decimal saldoDespesaAux = 0;
            decimal saldoReceitaAux = 0;
            
            try
            {
                var listaMovimentacao = await ListarMovimentacaoAsync();

                if (listaMovimentacao.Any())
                {
                    foreach(var item in listaMovimentacao)
                    {
                        saldoDespesaAux = saldoDespesa;
                        saldoReceitaAux = saldoReceita;

                        if (item.id_movimentacao == Despesa)
                        {
                            saldoDespesa = (saldoDespesaAux + item.vl_lancamento);
                        }

                        if (item.id_movimentacao == Receita)
                        {
                            saldoReceita = (saldoReceitaAux + item.vl_lancamento);
                        }
                    }

                    saldoTotal = (saldoReceita - saldoDespesa);

                    saldoMovimentacao.saldoTotal = saldoTotal;
                    saldoMovimentacao.saldoReceita = saldoReceita;
                    saldoMovimentacao.saldoDespesa = saldoDespesa;
                }
            }
            catch
            {
                return null;
            }
        

            return saldoMovimentacao;
        }
    }
}
