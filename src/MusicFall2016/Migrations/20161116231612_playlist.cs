using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MusicFall2016.Migrations
{
    public partial class playlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Playlist",
                columns: table => new
                {
                    PlaylistId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Playlist", x => x.PlaylistId);
                });

            migrationBuilder.AddColumn<DateTime>(
                name: "dateJoined",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PlaylistId",
                table: "Albums",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Albums_PlaylistId",
                table: "Albums",
                column: "PlaylistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Albums_Playlist_PlaylistId",
                table: "Albums",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "PlaylistId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Playlist_PlaylistId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_PlaylistId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "dateJoined",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Albums");

            migrationBuilder.DropTable(
                name: "Playlist");
        }
    }
}
