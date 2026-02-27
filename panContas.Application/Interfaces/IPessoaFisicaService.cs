using panContas.Domain.Entities;

namespace panContas.Application.Interfaces
{
    public interface IPessoaFisicaService
    {
        Task<PessoaFisica?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<PessoaFisica>> ObterTodosAsync();
        Task<PessoaFisica> CadastrarAsync(PessoaFisica pessoa, string cep);
        Task AtualizarAsync(PessoaFisica pessoa, string? cep = null);
        Task DeletarAsync(Guid id);
    }
}
