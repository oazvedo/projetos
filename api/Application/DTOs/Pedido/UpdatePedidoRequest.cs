using System.Text.Json.Serialization;
using api.Domain.Enums;

namespace api.Application.DTOs.Pedido
{
    public class UpdatePedidoRequest
    {
        [JsonPropertyName("status")]
        public required PedidoStatus Status { get; set; }
    }
}
