using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Phibra.Prova.Application.Services;
using Phibra.Prova.Application.Services.Interfaces;
using Phibra.Prova.Domain.Model;

namespace Phibra.Prova.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SaldoMovimentacaoController : ControllerBase
    {
        private readonly ISaldoMovimentacaoServices _saldoMovimentacaoServices;

        public SaldoMovimentacaoController(ISaldoMovimentacaoServices saldoMovimentacaoServices)
        {
            _saldoMovimentacaoServices = saldoMovimentacaoServices;
        }

        [HttpGet]
        [Route("ListarSaldoMovimentacao")]
        public async Task<IActionResult> ListarSaldoMovimentacao()
        {

            var response = await _saldoMovimentacaoServices.ListarSaldoMovimentacao();
            return Ok(response);

        }
    }
}
