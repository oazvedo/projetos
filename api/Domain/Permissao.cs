
using System.Text.Json.Serialization;

namespace api.domain
{
    public class Permissao
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Descricao { get; set; }

        [JsonIgnore]
        public ICollection<UsuarioPermissao> UsuarioPermissoes { get; set; } = new List<UsuarioPermissao>();
    }
}