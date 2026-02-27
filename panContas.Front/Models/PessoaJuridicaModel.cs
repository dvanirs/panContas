namespace panContas.Front.Models
{
    public class PessoaJuridicaModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public DateTime DataNascimento { get; set; } = DateTime.Today;
        public string Sexo { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public Guid EnderecoId { get; set; }
        public EnderecoModel Endereco { get; set; } = new EnderecoModel();
    }
}
