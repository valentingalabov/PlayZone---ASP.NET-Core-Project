namespace PlayZone.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class InitialCreate12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Chanels_Id",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Chanels",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chanels_UserId",
                table: "Chanels",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Chanels_AspNetUsers_UserId",
                table: "Chanels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chanels_AspNetUsers_UserId",
                table: "Chanels");

            migrationBuilder.DropIndex(
                name: "IX_Chanels_UserId",
                table: "Chanels");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Chanels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Chanels_Id",
                table: "AspNetUsers",
                column: "Id",
                principalTable: "Chanels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
