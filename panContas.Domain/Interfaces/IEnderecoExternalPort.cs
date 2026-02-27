using System.Threading.Tasks;
using panContas.Domain.Entities;

namespace panContas.Domain.Interfaces
{
    public interface IEnderecoExternalPort
    {
        Task<Endereco?> ConsultarCepAsync(string cep);
    }
}
