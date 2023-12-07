using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbaJohn.Migrations
{
    public partial class productseller : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SellerID",
                table: "products",
                type: "nvarchar(450)",
                nullable: true,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_products_SellerID",
                table: "products",
                column: "SellerID");

            migrationBuilder.AddForeignKey(
                name: "FK_products_AspNetUsers_SellerID",
                table: "products",
                column: "SellerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_products_AspNetUsers_SellerID",
                table: "products");

            migrationBuilder.DropIndex(
                name: "IX_products_SellerID",
                table: "products");

            migrationBuilder.DropColumn(
                name: "SellerID",
                table: "products");
        }
    }
}
