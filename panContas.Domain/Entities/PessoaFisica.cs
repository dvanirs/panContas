using System;

namespace panContas.Domain.Entities
{
    public class PessoaFisica : Entity
    {
        public string Nome { get; set; } = string.Empty;
        public string Cpf { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; }
        public string Sexo { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public Guid EnderecoId { get; set; }
        public Endereco Endereco { get; set; } = new Endereco();
    }
}
