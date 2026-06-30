using System.Text.Json.Serialization;
using api.Domain.Enums;

namespace api.Application.DTOs.Pedido
{
    public class UpdateStatusPedidoRequest
    {
        [JsonPropertyName("status")]
        public required PedidoStatus Status { get; set; }
        
        [JsonPropertyName("contratacao")]
        public PedidoTipoContratacaoEnum contratacao {get;set;}

        [JsonPropertyName("itens")]
        public List<CreatePedidoItemRequest> itens { get; set; } = new();
    }

    public class UpdatePedidoRequest
    {
        [JsonPropertyName("contratacao")]
        public PedidoTipoContratacaoEnum Contratacao { get; set; }

        [JsonPropertyName("status")]
        public required PedidoStatus Status { get; set; }

        [JsonPropertyName("itens")]
        public List<CreatePedidoItemRequest> Itens { get; set; } = new();
    }
}
