using api.Domain;
using api.Domain.Enums;
using api.infra;
using api.infra.repository;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace api.Tests.Repositories
{
    public class PedidoRepositoryTests
    {
        private static DatabaseContext CreateContext() =>
            new(new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

        [Fact]
        public async Task GetPedidosAsync_DeveRetornarTodosOsPedidos()
        {
            using var ctx = CreateContext();
            ctx.Pedidos.AddRange(
                new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal),
                new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Anual)
            );
            await ctx.SaveChangesAsync();

            var result = await new PedidoRepository(ctx).GetPedidosAsync();

            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPedidosByUsuarioIdAsync_DeveRetornarApenasDoUsuario()
        {
            using var ctx = CreateContext();
            var usuarioId = Guid.NewGuid();
            ctx.Pedidos.AddRange(
                new Pedido(usuarioId, PedidoTipoContratacaoEnum.Mensal),
                new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Anual)
            );
            await ctx.SaveChangesAsync();

            var result = await new PedidoRepository(ctx).GetPedidosByUsuarioIdAsync(usuarioId);

            Assert.Single(result);
            Assert.All(result, p => Assert.Equal(usuarioId, p.UsuarioId));
        }

        [Fact]
        public async Task GetPedidoById_QuandoExiste_DeveRetornarPedido()
        {
            using var ctx = CreateContext();
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);
            ctx.Pedidos.Add(pedido);
            await ctx.SaveChangesAsync();

            var result = await new PedidoRepository(ctx).GetPedidoById(pedido.Id);

            Assert.NotNull(result);
            Assert.Equal(pedido.Id, result.Id);
        }

        [Fact]
        public async Task GetPedidoById_QuandoNaoExiste_DeveRetornarNull()
        {
            using var ctx = CreateContext();

            var result = await new PedidoRepository(ctx).GetPedidoById(Guid.NewGuid());

            Assert.Null(result);
        }

        [Fact]
        public async Task AdicionarPedido_DevePersistirPedidoNoBanco()
        {
            using var ctx = CreateContext();
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Anual);

            var result = await new PedidoRepository(ctx).AdicionarPedido(pedido);

            Assert.Equal(1, await ctx.Pedidos.CountAsync());
            Assert.Equal(pedido.Id, result.Id);
            Assert.Equal(PedidoTipoContratacaoEnum.Anual, result.Contracacao);
        }

        [Fact]
        public async Task AtualizarPedido_QuandoExiste_DeveAtualizarStatus()
        {
            using var ctx = CreateContext();
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);
            ctx.Pedidos.Add(pedido);
            await ctx.SaveChangesAsync();

            pedido.UpdateStatus(PedidoStatus.EmProcessamento);
            var result = await new PedidoRepository(ctx).AtualizarPedido(pedido.Id, pedido);

            Assert.NotNull(result);
            Assert.Equal(PedidoStatus.EmProcessamento, result.Status);
        }

        [Fact]
        public async Task AtualizarPedido_QuandoExiste_DeveAtualizarContratacao()
        {
            using var ctx = CreateContext();
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);
            ctx.Pedidos.Add(pedido);
            await ctx.SaveChangesAsync();

            pedido.UpdateContratacao(PedidoTipoContratacaoEnum.Anual);
            var result = await new PedidoRepository(ctx).AtualizarPedido(pedido.Id, pedido);

            Assert.NotNull(result);
            Assert.Equal(PedidoTipoContratacaoEnum.Anual, result.Contracacao);
        }

        [Fact]
        public async Task AtualizarPedido_QuandoNaoExiste_DeveRetornarNull()
        {
            using var ctx = CreateContext();
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);

            var result = await new PedidoRepository(ctx).AtualizarPedido(Guid.NewGuid(), pedido);

            Assert.Null(result);
        }

        [Fact]
        public async Task RemoverPedido_QuandoExiste_DeveRemoverERetornarTrue()
        {
            using var ctx = CreateContext();
            var pedido = new Pedido(Guid.NewGuid(), PedidoTipoContratacaoEnum.Mensal);
            ctx.Pedidos.Add(pedido);
            await ctx.SaveChangesAsync();

            var result = await new PedidoRepository(ctx).RemoverPedido(pedido.Id);

            Assert.True(result);
            Assert.Equal(0, await ctx.Pedidos.CountAsync());
        }

        [Fact]
        public async Task RemoverPedido_QuandoNaoExiste_DeveRetornarFalse()
        {
            using var ctx = CreateContext();

            var result = await new PedidoRepository(ctx).RemoverPedido(Guid.NewGuid());

            Assert.False(result);
        }
    }
}
