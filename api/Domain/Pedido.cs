using api.Domain.Enums;
using Newtonsoft.Json;

namespace api.Domain
{
    public class Pedido
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("status")]
        public PedidoStatus Status { get; set; }

        [JsonProperty("contratacao")]
        public PedidoTipoContratacaoEnum Contracacao { get; set; }

        [JsonProperty("usuario_id")]
        public Guid UsuarioId { get; set; }

        [JsonProperty("produto_id")]
        public Guid ProdutoId { get; set; }

        [JsonProperty("produto")]
        public Produto? Produto { get; set; }

        [JsonProperty("criado_em")]
        public DateTime CriadoEm { get; set; }

        [JsonProperty("atualizado_em")]
        public DateTime? AtualizadoEm { get; set; }

        public Pedido() { }

        public Pedido(Guid usuarioId, Guid produtoId, PedidoTipoContratacaoEnum contratacao)
        {
            Id = Guid.NewGuid();
            Status = PedidoStatus.Criado;
            Contracacao = contratacao;
            UsuarioId = usuarioId;
            ProdutoId = produtoId;
            CriadoEm = DateTime.UtcNow;
        }

        public void UpdatePedido(PedidoTipoContratacaoEnum newContratacao, PedidoStatus newStatus)
        {
            ValidarStatus();
            Status = newStatus;
            Contracacao = newContratacao;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void UpdateStatus(PedidoStatus novoStatus)
        {
            ValidarStatus();
            Status = novoStatus;
            AtualizadoEm = DateTime.UtcNow;
        }

        public void UpdateContratacao(PedidoTipoContratacaoEnum novaContratacao)
        {
            ValidarStatus();
            Contracacao = novaContratacao;
            AtualizadoEm = DateTime.UtcNow;
        }

        private void ValidarStatus()
        {
            if (Status == PedidoStatus.Cancelado)
                throw new InvalidOperationException("Pedidos cancelados não podem ter atualização de status");
        }
    }
}
