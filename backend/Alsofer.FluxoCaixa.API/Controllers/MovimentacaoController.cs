using Alsofer.FluxoCaixa.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phibra.Prova.Application.Services.Interfaces;

namespace Phibra.Prova.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly IMovimentacaoServices _movimentacaoServices;

        public MovimentacaoController(IMovimentacaoServices movimentacaoServices)
        {
            _movimentacaoServices = movimentacaoServices;
        }

        [HttpGet]
        [Route("ListarMovimentacao")]
        public async Task<IActionResult> ListarMovimentacao()
        {

            var response = await _movimentacaoServices.ListarMovimentacao();

            return Ok(response);

        }

        [HttpGet]
        [Route("ListarMovimentacaoId")]
        public async Task<IActionResult> ListarMovimentacaoId(int id)
        {

            var response = await _movimentacaoServices.ListarMovimentacao(id);

            return Ok(response);

        }


        [HttpPost]
        [Route("AdicionarMovimentacao")]
        public async Task<IActionResult> AdicionarMovimentacao(MovimentacaoRequest movimentacaoRequest)
        {

            var response = await _movimentacaoServices.AdicionarMovimentacao(movimentacaoRequest);

            return Ok(response);

        }

        [HttpPost]
        [Route("AtualizarMovimentacao")]
        [Authorize]
        public async Task<IActionResult> AtualizarMovimentacao(MovimentacaoRequest movimentacaoRequest)
        {

            var response = await _movimentacaoServices.AtualizarMovimentacao(movimentacaoRequest);

            return Ok(response);

        }

        [HttpPost]
        [Route("ExcluirMovimentacao")]
        [Authorize]
        public async Task<IActionResult> ExcluirMovimentacao(int id)
        {

            var response = await _movimentacaoServices.ExcluirMovimentacao(id);

            return Ok(response);

        }
    }
}
