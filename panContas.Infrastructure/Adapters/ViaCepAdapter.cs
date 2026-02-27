using System.Text.Json;
using panContas.Domain.Entities;
using panContas.Domain.Interfaces;

namespace panContas.Infrastructure.Adapters
{
    public class ViaCepAdapter : IEnderecoExternalPort
    {
        private readonly HttpClient _httpClient;

        public ViaCepAdapter(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Endereco?> ConsultarCepAsync(string cep)
        {
            if (string.IsNullOrWhiteSpace(cep)) return null;

            cep = cep.Replace("-", "").Replace(".", "");

            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var viaCepResult = JsonSerializer.Deserialize<ViaCepResponse>(content, options);

            if (viaCepResult == null || viaCepResult.Erro)
                return null;

            return new Endereco
            {
                CEP = viaCepResult.Cep,
                Logradouro = viaCepResult.Logradouro,
                Complemento = viaCepResult.Complemento,
                Unidade = viaCepResult.Unidade,
                Bairro = viaCepResult.Bairro,
                Localidade = viaCepResult.Localidade,
                UF = viaCepResult.Uf,
                Estado = viaCepResult.Estado,
                Regiao = viaCepResult.Regiao,
                IBGE = viaCepResult.Ibge,
                GIA = viaCepResult.Gia,
                DDD = viaCepResult.Ddd,
                SIAFI = viaCepResult.Siafi
            };
        }

        private class ViaCepResponse
        {
            public string? Cep { get; set; }
            public string? Logradouro { get; set; }
            public string? Complemento { get; set; }
            public string? Unidade { get; set; }
            public string? Bairro { get; set; }
            public string? Localidade { get; set; }
            public string? Uf { get; set; }
            public string? Estado { get; set; }
            public string? Regiao { get; set; }
            public string? Ibge { get; set; }
            public string? Gia { get; set; }
            public string? Ddd { get; set; }
            public string? Siafi { get; set; }
            public bool Erro { get; set; }
        }
    }
}
