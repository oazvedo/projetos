using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddProduto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "pedidos",
                newName: "produto_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pedidos_produtos_produto_id",
                table: "pedidos");

            migrationBuilder.DropIndex(
                name: "IX_pedidos_produto_id",
                table: "pedidos");

            migrationBuilder.RenameColumn(
                name: "produto_id",
                table: "pedidos",
                newName: "ProdutoId");
        }
    }
}
