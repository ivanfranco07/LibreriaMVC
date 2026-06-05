using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebMvc.Migrations
{
    /// <inheritdoc />
    public partial class ArregloFk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_AspNetUsers_UsuarioId1",
                table: "Favoritos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UsuarioId1",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UsuarioId1",
                table: "Favoritos");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UsuarioId1",
                table: "Favoritos");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "UsuarioId",
                table: "Favoritos",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsuarioId",
                table: "Reviews",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UsuarioId",
                table: "Favoritos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_AspNetUsers_UsuarioId",
                table: "Favoritos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId",
                table: "Reviews",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favoritos_AspNetUsers_UsuarioId",
                table: "Favoritos");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_UsuarioId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Favoritos_UsuarioId",
                table: "Favoritos");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Reviews",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Favoritos",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId1",
                table: "Favoritos",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_UsuarioId1",
                table: "Reviews",
                column: "UsuarioId1");

            migrationBuilder.CreateIndex(
                name: "IX_Favoritos_UsuarioId1",
                table: "Favoritos",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Favoritos_AspNetUsers_UsuarioId1",
                table: "Favoritos",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_AspNetUsers_UsuarioId1",
                table: "Reviews",
                column: "UsuarioId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
