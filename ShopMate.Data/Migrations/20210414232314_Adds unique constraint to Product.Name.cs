using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopMate.Data.Migrations
{
    public partial class AddsuniqueconstrainttoProductName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "UX_Product_Name",
                table: "Product",
                column: "Name",
                unique: true)
                .Annotation("SqlServer:Clustered", false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UX_Product_Name",
                table: "Product");
        }
    }
}
