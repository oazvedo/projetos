using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddPedidoItens : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_produtos_produto_id",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_produto_id",
                table: "pedidos");

            migrationBuilder.DropColumn(
                name: "produto_id",
                table: "pedidos");

            migrationBuilder.AddColumn<decimal>(
                name: "Preco",
                table: "produtos",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "pedido_itens",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    pedido_id = table.Column<Guid>(type: "uuid", nullable: false),
                    produto_id = table.Column<Guid>(type: "uuid", nullable: false),
                    quantidade = table.Column<int>(type: "integer", nullable: false),
                    preco_unitario = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pedido_itens", x => x.id);
                    table.ForeignKey(
                        name: "FK_pedido_itens_pedidos_pedido_id",
                        column: x => x.pedido_id,
                        principalTable: "pedidos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_pedido_itens_produtos_produto_id",
                        column: x => x.produto_id,
                        principalTable: "produtos",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_pedido_itens_pedido_id",
                table: "pedido_itens",
                column: "pedido_id");

            migrationBuilder.CreateIndex(
                name: "IX_pedido_itens_produto_id",
                table: "pedido_itens",
                column: "produto_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pedido_itens");

            migrationBuilder.DropColumn(
                name: "Preco",
                table: "produtos");

            migrationBuilder.AddColumn<Guid>(
                name: "produto_id",
                table: "pedidos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_pedidos_produto_id",
                table: "pedidos",
                column: "produto_id");

            migrationBuilder.AddForeignKey(
                name: "FK_pedidos_produtos_produto_id",
                table: "pedidos",
                column: "produto_id",
                principalTable: "produtos",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
