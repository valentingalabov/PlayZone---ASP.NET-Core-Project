using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayZone.Data.Migrations
{
    public partial class AddImageToChanel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Chanels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Chanels");
        }
    }
}
