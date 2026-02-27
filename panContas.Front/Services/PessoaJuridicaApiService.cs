using System.Net.Http.Json;
using panContas.Front.Models;

namespace panContas.Front.Services
{
    public class PessoaJuridicaApiService
    {
        private readonly HttpClient _http;

        public PessoaJuridicaApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<PessoaJuridicaModel>> ObterTodosAsync()
        {
            return await _http.GetFromJsonAsync<List<PessoaJuridicaModel>>("api/v1/PessoaJuridica") ?? new();
        }

        public async Task<PessoaJuridicaModel?> ObterPorIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<PessoaJuridicaModel>($"api/v1/PessoaJuridica/{id}");
        }

        public async Task<EnderecoModel?> ConsultarCepAsync(string cep)
        {
            var response = await _http.GetAsync($"api/v1/Endereco/cep/{cep}");
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<EnderecoModel>();
            return null;
        }

        public async Task<PessoaJuridicaModel> CadastrarAsync(PessoaJuridicaModel pessoa, string cep)
        {
            var response = await _http.PostAsJsonAsync($"api/v1/PessoaJuridica?cep={cep}", pessoa);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<PessoaJuridicaModel>() ?? pessoa;
        }

        public async Task AtualizarAsync(PessoaJuridicaModel pessoa, string? cep = null)
        {
            var url = string.IsNullOrEmpty(cep)
                ? $"api/v1/PessoaJuridica/{pessoa.Id}"
                : $"api/v1/PessoaJuridica/{pessoa.Id}?cep={cep}";
            var response = await _http.PutAsJsonAsync(url, pessoa);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeletarAsync(Guid id)
        {
            var response = await _http.DeleteAsync($"api/v1/PessoaJuridica/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
