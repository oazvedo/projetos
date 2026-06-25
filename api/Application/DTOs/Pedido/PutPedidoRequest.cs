using System.Text.Json.Serialization;
using api.Domain.Enums;

namespace api.Application.DTOs.Pedido
{
    public class PutPedidoRequest
    {
        [JsonPropertyName("contratacao")]
        public required PedidoTipoContratacaoEnum Contratacao { get; set; }
    }
}
