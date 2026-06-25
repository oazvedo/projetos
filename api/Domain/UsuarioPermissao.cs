using System.Text.Json.Serialization;

namespace api.domain
{
    public class UsuarioPermissao
    {
        public Guid UsuarioId { get; set; }

        [JsonIgnore]
        public Usuario Usuario { get; set; } = null!;

        public Guid PermissaoId { get; set; }
        public Permissao Permissao { get; set; } = null!;
    }
}
