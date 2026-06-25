using System.Text.Json.Serialization;

namespace api.Application.DTOs.Permissao
{
    public class PermissaoDto
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("nome")]
        public string Nome { get; set; } = null!;

        [JsonPropertyName("descricao")]
        public string Descricao { get; set; } = null!;
    }
}
