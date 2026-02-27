using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using panContas.Application.Interfaces;
using panContas.Domain.Entities;

namespace panContas.API.Controllers.v1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PessoaJuridicaController : ControllerBase
    {
        private readonly IPessoaJuridicaService _service;

        public PessoaJuridicaController(IPessoaJuridicaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.ObterTodosAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var result = await _service.ObterPorIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PessoaJuridica pessoa, [FromQuery] string cep)
        {
            var result = await _service.CadastrarAsync(pessoa, cep);
            return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PessoaJuridica pessoa, [FromQuery] string? cep)
        {
            if (id != pessoa.Id) return BadRequest();
            await _service.AtualizarAsync(pessoa, cep);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeletarAsync(id);
            return NoContent();
        }
    }
}
