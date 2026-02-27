using panContas.Application.Interfaces;
using panContas.Domain.Entities;
using panContas.Domain.Interfaces;

namespace panContas.Application.Services
{
    public class PessoaJuridicaService : IPessoaJuridicaService
    {
        private readonly IPessoaJuridicaRepository _repository;
        private readonly IEnderecoExternalPort _enderecoExternalPort;

        public PessoaJuridicaService(IPessoaJuridicaRepository repository, IEnderecoExternalPort enderecoExternalPort)
        {
            _repository = repository;
            _enderecoExternalPort = enderecoExternalPort;
        }

        public async Task<IEnumerable<PessoaJuridica>> ObterTodosAsync() => await _repository.GetAllAsync();

        public async Task<PessoaJuridica?> ObterPorIdAsync(Guid id) => await _repository.GetByIdAsync(id);

        public async Task<PessoaJuridica> CadastrarAsync(PessoaJuridica pessoa, string cep)
        {
            if (pessoa.Endereco == null || string.IsNullOrWhiteSpace(pessoa.Endereco.CEP))
            {
                var endereco = await _enderecoExternalPort.ConsultarCepAsync(cep);
                pessoa.Endereco = endereco ?? new Endereco();
            }

            await _repository.AddAsync(pessoa);
            return pessoa;
        }

        public async Task AtualizarAsync(PessoaJuridica pessoa, string? cep = null)
        {
            if (pessoa.Endereco == null && !string.IsNullOrWhiteSpace(cep))
            {
                var endereco = await _enderecoExternalPort.ConsultarCepAsync(cep);
                pessoa.Endereco = endereco ?? new Endereco();
            }
            else if (pessoa.Endereco == null)
            {
                pessoa.Endereco = new Endereco();
            }
            
            await _repository.UpdateAsync(pessoa);
        }

        public async Task DeletarAsync(Guid id) => await _repository.DeleteAsync(id);
    }
}
