using System.Text.Json.Serialization;
using api.Domain.Enums;

namespace api.Application.DTOs.Pedido
{
    public class UpdateStatusPedidoRequest
    {
        [JsonPropertyName("status")]
        public required PedidoStatus Status { get; set; }
    }

    public class UpdatePedidoRequest
    {
        [JsonPropertyName("contratacao")]
        public PedidoTipoContratacaoEnum Contratacao {get; set;}

        [JsonPropertyName("status")]
        public required PedidoStatus Status { get; set; }
    }
}
