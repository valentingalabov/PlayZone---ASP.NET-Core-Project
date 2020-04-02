using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayZone.Data.Migrations
{
    public partial class AddColoumToHistoryAndFavorites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "VideoHistories",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "VideoHistories",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "VideoHistories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "VideoHistories",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "VideoHistories",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "FavoriteVideos",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "FavoriteVideos",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "FavoriteVideos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FavoriteVideos",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "FavoriteVideos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VideoHistories_IsDeleted",
                table: "VideoHistories",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteVideos_IsDeleted",
                table: "FavoriteVideos",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_VideoHistories_IsDeleted",
                table: "VideoHistories");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteVideos_IsDeleted",
                table: "FavoriteVideos");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "VideoHistories");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "VideoHistories");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "VideoHistories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "VideoHistories");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "VideoHistories");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "FavoriteVideos");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "FavoriteVideos");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "FavoriteVideos");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FavoriteVideos");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "FavoriteVideos");
        }
    }
}
