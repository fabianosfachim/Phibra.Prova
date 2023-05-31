using Microsoft.AspNetCore.Mvc;
using Phibra.Prova.Application.Services.Interfaces;


namespace Phibra.Prova.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoMovimentacaoController : ControllerBase
    {
        private readonly ITipoMovimentacaoServices _tipoMovimentacaoServices;
        public TipoMovimentacaoController(ITipoMovimentacaoServices tipoMovimentacaoServices)
        {
            _tipoMovimentacaoServices = tipoMovimentacaoServices;
        }

        [HttpGet]
        [Route("ListarTipoMovimentacao")]
        public async Task<IActionResult> ListarDespesa()
        {

            var response = await _tipoMovimentacaoServices.ListarTipoMovimentacao();

            return Ok(response);

        }

       

    }
}
