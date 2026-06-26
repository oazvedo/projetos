using System.Text.Json.Serialization;
using api.Domain.Enums;
using Newtonsoft.Json;

namespace api.Application.DTOs.Pedido
{
    public class CreatePedidoRequest
    {
        [JsonPropertyName("contratacao")]
        public PedidoTipoContratacaoEnum contratacao {get;set;}

        [JsonPropertyName("produto_id")]
        public Guid produtoId {get; set; }
    }
}
