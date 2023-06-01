using Phibra.Prova.Application.Services.Interfaces;
using Phibra.Prova.Application.Services.Wrappers;
using Phibra.Prova.Data.Interfaces;
using Phibra.Prova.Data.Repositories.Entities;
using Phibra.Prova.Domain.Entities;
using Phibra.Prova.Domain.Model;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace Phibra.Prova.Application.Services
{
    public class SaldoMovimentacaoServices : ISaldoMovimentacaoServices
    {
        private readonly ISaldoMovimentacaoRepository _saldoMovimentacaoRepository;
        private readonly IMovimentacaoServices _movimentacaoServices;
        private const int Receita = 1;
        private const int Despesa = 2;

        public SaldoMovimentacaoServices(ISaldoMovimentacaoRepository saldoMovimentacaoRepository,
                                         IMovimentacaoServices movimentacaoServices)
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
                    var saldoMovimentacao = await SaldoMovimentacaoAsync();

                    if (saldoMovimentacao == null)
                    {
                        listaTotalSaldoMovimentacao.dt_movimentacao = DateTime.Now;
                        await AdicionarSaldoMovimentacaoAsync(listaTotalSaldoMovimentacao);
                        saldoMovimentacaoResponse.saldoMovimentacao = listaTotalSaldoMovimentacao;
                    }
                    else
                    {
                        if (listaTotalSaldoMovimentacao.saldoTotal != saldoMovimentacao.saldoTotal)
                        {
                            //Adicionar um novo regitro na tabela
                            SaldoMovimentacao saldo = new SaldoMovimentacao();
                            saldo.dt_movimentacao = DateTime.Now;
                            saldo.saldoReceita = listaTotalSaldoMovimentacao.saldoReceita;
                            saldo.saldoDespesa = listaTotalSaldoMovimentacao.saldoDespesa;
                            saldo.saldoTotal = listaTotalSaldoMovimentacao.saldoTotal;

                            await AdicionarSaldoMovimentacaoAsync(saldo);
                            saldoMovimentacaoResponse.saldoMovimentacao = listaTotalSaldoMovimentacao;
                        }
                    }

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
                saldoMovimentacao = _saldoMovimentacaoRepository.GetAllAsync().Result.ToList().LastOrDefault();

            }
            catch
            {
                return null;
            }

            return saldoMovimentacao;
        }

        private async Task AdicionarSaldoMovimentacaoAsync(SaldoMovimentacao saldoMovimentacaoRequest)
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();
            SaldoMovimentacao saldoMovimentacao = new SaldoMovimentacao();
            int id = int.MinValue;

            try
            {
                await _saldoMovimentacaoRepository.AddAsync(saldoMovimentacaoRequest);
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
