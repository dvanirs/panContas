using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using panContas.Domain.Interfaces;

namespace panContas.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoExternalPort _enderecoExternalPort;

        public EnderecoController(IEnderecoExternalPort enderecoExternalPort)
        {
            _enderecoExternalPort = enderecoExternalPort;
        }

        [HttpGet("cep/{cep}")]
        public async Task<IActionResult> GetByCep(string cep)
        {
            var result = await _enderecoExternalPort.ConsultarCepAsync(cep);
            if (result == null) return NotFound($"CEP '{cep}' não encontrado.");
            return Ok(result);
        }
    }
}
