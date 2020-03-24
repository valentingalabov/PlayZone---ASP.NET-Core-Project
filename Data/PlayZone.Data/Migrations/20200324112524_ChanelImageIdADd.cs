namespace PlayZone.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class ChanelImageIdADd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Chanels",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Chanels");
        }
    }
}
