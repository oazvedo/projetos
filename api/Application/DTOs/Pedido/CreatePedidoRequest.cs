using api.Domain.Enums;

namespace api.Application.DTOs.Pedido
{
    public class CreatePedidoRequest
    {
        public PedidoTipoContratacaoEnum contratacao {get;set;}
    }
}
