using Phibra.Prova.Application.Services.Interfaces;
using Phibra.Prova.Application.Services.Wrappers;
using Phibra.Prova.Data.Interfaces;
using Phibra.Prova.Domain.Model;

namespace Phibra.Prova.Application.Services
{
    public class MovimentacaoServices : IMovimentacaoServices
    {
        private readonly IMovimentacaoRepository _movimentacaoRepository;

        public MovimentacaoServices(IMovimentacaoRepository movimentacaoRepository)
        {
            _movimentacaoRepository = movimentacaoRepository;
        }

        public async Task<Response<MovimentacaoResponse>> AdicionarMovimentacao(MovimentacaoRequest movimentacaoRequest)
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();

            try
            {

                await _movimentacaoRepository.AddAsync(movimentacaoRequest.movimentacao);

                //Id para chamar o cadastro do endereço
                var idMovimentacao = movimentacaoRequest.movimentacao.id;

                if (idMovimentacao > 0)
                {
                    movimentacaoResponse.objmovimentacao = movimentacaoRequest.movimentacao;
                    movimentacaoResponse.Executado = true;
                    movimentacaoResponse.MensagemRetorno = "Cadastro de movimento efetuada com sucesso";
                }
            }

            catch
            {
                movimentacaoResponse.Executado = false;
                movimentacaoResponse.MensagemRetorno = "Erro ao cadastrar o movimento";
            }

            return new Response<MovimentacaoResponse>(movimentacaoResponse, $"Cadastro Movimento.");
        }

        public async Task<Response<MovimentacaoResponse>> AtualizarMovimentacao(MovimentacaoRequest movimentacaoRequest)
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();

            try
            {

                await _movimentacaoRepository.UpdateAsync(movimentacaoRequest.movimentacao);

                //Id para chamar o cadastro do endereço
                var idMovimentacao = movimentacaoRequest.movimentacao.id;

                if (idMovimentacao > 0)
                {
                    movimentacaoResponse.objmovimentacao = movimentacaoRequest.movimentacao;
                    movimentacaoResponse.Executado = true;
                    movimentacaoResponse.MensagemRetorno = "Atualização do Cadastro de movimento efetuada com sucesso";
                }
            }
            catch
            {
                movimentacaoResponse.Executado = false;
                movimentacaoResponse.MensagemRetorno = "Erro ao atualizar o cadastro o movimento";
            }

            return new Response<MovimentacaoResponse>(movimentacaoResponse, $"Atualização do Cadastro Movimento.");
        }

        public async Task<Response<MovimentacaoResponse>> ExcluirMovimentacao(int id)
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();

            try
            {

                await _movimentacaoRepository.RemoveAsync(id);

                movimentacaoResponse.Executado = true;
                movimentacaoResponse.MensagemRetorno = "Exclusão do Cadastro de movimento efetuada com sucesso";
            }

            catch
            {
                movimentacaoResponse.Executado = false;
                movimentacaoResponse.MensagemRetorno = "Erro ao excluir o cadastro o movimento";
            }

            return new Response<MovimentacaoResponse>(movimentacaoResponse, $"Atualização do Cadastro Movimento.");
        }

        public async Task<Response<MovimentacaoResponse>> ListarMovimentacao()
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();

            try
            {
                var listaMovimentacao = await _movimentacaoRepository.GetAllAsync();

                if (listaMovimentacao.Any())
                {
                    movimentacaoResponse.movimentacao = listaMovimentacao.ToList();
                    movimentacaoResponse.Executado = true;
                    movimentacaoResponse.MensagemRetorno = "Consulta de movimentação efetuada com sucesso";
                }
                else
                {
                    movimentacaoResponse.Executado = true;
                    movimentacaoResponse.MensagemRetorno = "Não existem movimentação cadastrados no banco de dados";
                }
            }
            catch
            {
                movimentacaoResponse.Executado = false;
                movimentacaoResponse.MensagemRetorno = "Erro na consulta de movimentação";
            }

            return new Response<MovimentacaoResponse>(movimentacaoResponse, $"Lista movimentação.");
        }

        public async Task<Response<MovimentacaoResponse>> ListarMovimentacao(int id)
        {
            MovimentacaoResponse movimentacaoResponse = new MovimentacaoResponse();

            try
            {
                var listaMovimentacao = await _movimentacaoRepository.GetByIdAsync(id);

                if (listaMovimentacao != null)
                {
                    movimentacaoResponse.objmovimentacao = listaMovimentacao;
                    movimentacaoResponse.Executado = true;
                    movimentacaoResponse.MensagemRetorno = "Consulta de movimentação efetuada com sucesso";
                }
                else
                {
                    movimentacaoResponse.Executado = true;
                    movimentacaoResponse.MensagemRetorno = "Não existem movimentação cadastrados no banco de dados";
                }
            }
            catch
            {
                movimentacaoResponse.Executado = false;
                movimentacaoResponse.MensagemRetorno = "Erro na consulta de movimentação";
            }

            return new Response<MovimentacaoResponse>(movimentacaoResponse, $"Lista movimentação.");
        }
    }
}
