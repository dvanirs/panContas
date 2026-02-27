using System;

namespace panContas.Domain.Entities
{
    public class Endereco : Entity
    {
        public string? CEP { get; set; } = string.Empty;
        public string? Logradouro { get; set; } = string.Empty;
        public string? Complemento { get; set; } = string.Empty;
        public string? Unidade { get; set; } = string.Empty;        
        public string? Bairro { get; set; } = string.Empty;
        public string? Localidade { get; set; } = string.Empty;
        public string? UF { get; set; } = string.Empty;
        public string? Estado { get; set; } = string.Empty;
        public string? Regiao { get; set; } = string.Empty;
        public string? IBGE { get; set; } = string.Empty;
        public string? GIA { get; set; } = string.Empty;
        public string? DDD { get; set; } = string.Empty;
        public string? SIAFI { get; set; } = string.Empty;
    }
}
