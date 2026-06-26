namespace api.Application.DTOs.Pedido
{
    public class PedidoItemDto
    {
        public Guid ProdutoId { get; set; }
        public string NomeProduto { get; set; } = null!;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}
