using System.Net.Http.Json;
using panContas.Front.Models;

namespace panContas.Front.Services
{
    public class PessoaFisicaApiService
    {
        private readonly HttpClient _http;

        public PessoaFisicaApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PessoaFisicaModel>> ObterTodosAsync()
        {
            return await _http.GetFromJsonAsync<List<PessoaFisicaModel>>("api/v1/PessoaFisica") ?? new();
        }

        public async Task<PessoaFisicaModel?> ObterPorIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<PessoaFisicaModel>($"api/v1/PessoaFisica/{id}");
        }

        public async Task<EnderecoModel?> ConsultarCepAsync(string cep)
        {
            var response = await _http.GetAsync($"api/v1/Endereco/cep/{cep}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<EnderecoModel>();
            return null;
        }

        public async Task<PessoaFisicaModel> CadastrarAsync(PessoaFisicaModel pessoa, string cep)
        {
            var response = await _http.PostAsJsonAsync($"api/v1/PessoaFisica?cep={cep}", pessoa);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PessoaFisicaModel>() ?? pessoa;
        }

        public async Task AtualizarAsync(PessoaFisicaModel pessoa, string? cep = null)
        {
            var url = string.IsNullOrEmpty(cep)
                ? $"api/v1/PessoaFisica/{pessoa.Id}"
                : $"api/v1/PessoaFisica/{pessoa.Id}?cep={cep}";
            var response = await _http.PutAsJsonAsync(url, pessoa);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletarAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/v1/PessoaFisica/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
