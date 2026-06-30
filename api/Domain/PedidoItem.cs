using Newtonsoft.Json;

namespace api.Domain
{
    public class PedidoItem
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("pedido_id")]
        public Guid PedidoId { get; set; }

        [JsonProperty("produto_id")]
        public Guid ProdutoId { get; set; }

        [JsonProperty("produto")]
        public Produto Produto { get; set; } = null!;

        [JsonProperty("quantidade")]
        public int Quantidade { get; set; }

        [JsonProperty("preco_unitario")]
        public decimal PrecoUnitario { get; set; }

        [JsonProperty("subtotal")]
        public decimal Subtotal => Quantidade * PrecoUnitario;

        public PedidoItem() { }

        public PedidoItem(Guid pedidoId, Produto produto, int quantidade)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            ProdutoId = produto.Id;
            Produto = produto;
            Quantidade = quantidade;
            PrecoUnitario = produto.Preco;
        }
    }
}
