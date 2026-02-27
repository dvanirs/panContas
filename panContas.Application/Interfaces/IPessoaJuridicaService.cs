using panContas.Domain.Entities;

namespace panContas.Application.Interfaces
{
    public interface IPessoaJuridicaService
    {
        Task<PessoaJuridica?> ObterPorIdAsync(Guid id);
        Task<IEnumerable<PessoaJuridica>> ObterTodosAsync();
        Task<PessoaJuridica> CadastrarAsync(PessoaJuridica pessoa, string cep);
        Task AtualizarAsync(PessoaJuridica pessoa, string? cep = null);
        Task DeletarAsync(Guid id);
    }
}
