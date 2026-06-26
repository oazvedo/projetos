using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace api.Domain
{
    public class Produto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public required string Nome { get; set; }

        [JsonPropertyName("descricao")]
        public required string Descricao { get; set; }

        [JsonPropertyName("codigo_interno")]
        public required string Codigo { get; set; }

        [JsonPropertyName("status")]
        public bool Status {get; set;}

        [JsonPropertyName("criado_em")]
        public DateTime CriadoEm { get; set; }

        [JsonPropertyName("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }

        public Produto(string nome, string descricao)
        {
            Id = Guid.NewGuid();
            Nome = nome;
            Descricao = descricao;
            Status = true;
            CriadoEm = DateTime.UtcNow;
            ValidarProduto();
        }

        public void AtualizarProduto(string nome, string descricao, bool status, string codigo)
        {
            Nome = nome;
            Descricao = descricao;
            Status = status;
            Codigo = codigo;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void ValidarProduto()
        {
            if (string.IsNullOrWhiteSpace(Nome))
                throw new InvalidOperationException("Nome do produto não pode estar vazio.");
            
            if (string.IsNullOrWhiteSpace(Descricao))
                throw new InvalidOperationException("Descricao do produto não pode estar vazia.");

            if (string.IsNullOrWhiteSpace(Codigo))
                throw new InvalidOperationException("Codigo do produto não pode estar vazio.");
        }
    }
}