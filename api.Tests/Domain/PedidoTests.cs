using api.Domain;
using api.Domain.Enums;
using Xunit;

namespace api.Tests.Domain
{
    public class PedidoTests
    {
        [Fact]
        public void Constructor_DeveInicializarCorretamente()
        {
            var usuarioId = Guid.NewGuid();

            var pedido = new Pedido(usuarioId, PedidoTipoContratacaoEnum.Mensal);

            Assert.NotEqual(Guid.Empty, pedido.Id);
            Assert.Equal(usuarioId, pedido.UsuarioId);
            Assert.Equal(PedidoStatus.Criado, pedido.Status);
            Assert.Equal(PedidoTipoContratacaoEnum.Mensal, pedido.Contracacao);
            Assert.Null(pedido.AtualizadoEm);
        }

        [Fact]
        public void UpdateStatus_DeveAlterarStatusESetarAtualizadoEm()
        {
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);

            pedido.UpdateStatus(PedidoStatus.EmProcessamento);

            Assert.Equal(PedidoStatus.EmProcessamento, pedido.Status);
            Assert.NotNull(pedido.AtualizadoEm);
        }

        [Theory]
        [InlineData(PedidoStatus.EmProcessamento)]
        [InlineData(PedidoStatus.Suporte)]
        [InlineData(PedidoStatus.Finalizado)]
        public void UpdateStatus_QuandoCancelado_DeveLancarInvalidOperationException(PedidoStatus novoStatus)
        {
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);
            pedido.UpdateStatus(PedidoStatus.Cancelado);

            Assert.Throws<InvalidOperationException>(() => pedido.UpdateStatus(novoStatus));
        }

        [Fact]
        public void UpdateContratacao_DeveAlterarContratacaoESetarAtualizadoEm()
        {
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);

            pedido.UpdateContratacao(PedidoTipoContratacaoEnum.Anual);

            Assert.Equal(PedidoTipoContratacaoEnum.Anual, pedido.Contracacao);
            Assert.NotNull(pedido.AtualizadoEm);
        }

        [Fact]
        public void UpdateContratacao_QuandoCancelado_DeveLancarInvalidOperationException()
        {
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);
            pedido.UpdateStatus(PedidoStatus.Cancelado);

            Assert.Throws<InvalidOperationException>(() => pedido.UpdateContratacao(PedidoTipoContratacaoEnum.Anual));
        }
    }
}
