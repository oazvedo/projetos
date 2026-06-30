
using System.Text.Json.Serialization;
using api.domain;
using Newtonsoft.Json;

namespace api.Domain
{
    public class Carteira
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }
        [JsonPropertyName("usuario_id")]
        public Guid UsuarioId { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;

        [JsonPropertyName("saldo")]
        public double Saldo { get; set; } = 0;

        [JsonPropertyName("criado_em")]
        public DateTime CriadoEm {get; set;}

        [JsonPropertyName("atualizado_em")]
        public DateTime? AtualizadoEm {get; set;}

        public Carteira()
        {
        }

        public Carteira(Guid usuarioId)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            CriadoEm = DateTime.UtcNow;
        }

        public void UpdateBalance(double input)
        {
            Saldo += input;
            AtualizadoEm = DateTime.UtcNow;
        }


    }
}