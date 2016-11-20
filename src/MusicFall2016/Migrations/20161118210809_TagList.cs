using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicFall2016.Migrations
{
    public partial class TagList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Albums_Playlist_PlaylistId",
                table: "Albums");

            migrationBuilder.DropIndex(
                name: "IX_Albums_PlaylistId",
                table: "Albums");

            migrationBuilder.DropColumn(
                name: "PlaylistId",
                table: "Albums");

            migrationBuilder.CreateTable(
                name: "PlaylistTag",
                columns: table => new
                {
                    AlbumId = table.Column<int>(nullable: false),
                    PlaylistId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaylistTag", x => new { x.AlbumId, x.PlaylistId });
                    table.ForeignKey(
                        name: "FK_PlaylistTag_Albums_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Albums",
                        principalColumn: "AlbumID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlaylistTag_Playlist_PlaylistId",
                        column: x => x.PlaylistId,
                        principalTable: "Playlist",
                        principalColumn: "PlaylistId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistTag_AlbumId",
                table: "PlaylistTag",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_PlaylistTag_PlaylistId",
                table: "PlaylistTag",
                column: "PlaylistId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlaylistTag");

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
    }
}
