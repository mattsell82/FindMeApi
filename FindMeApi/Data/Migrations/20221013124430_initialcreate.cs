using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMeApi.Data.Migrations
{
    public partial class initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TrackId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Accuracy = table.Column<double>(type: "float", nullable: true),
                    ReducedAccuracy = table.Column<bool>(type: "bit", nullable: true),
                    VerticalAccuracy = table.Column<double>(type: "float", nullable: true),
                    Altitude = table.Column<double>(type: "float", nullable: true),
                    Course = table.Column<double>(type: "float", nullable: true),
                    IsFromMockProvider = table.Column<bool>(type: "bit", nullable: true),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    TimeStamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    TrackId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locations_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrackId",
                table: "AspNetUsers",
                column: "TrackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_TrackId",
                table: "Locations",
                column: "TrackId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tracks_TrackId",
                table: "AspNetUsers",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tracks_TrackId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrackId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "AspNetUsers");
        }
    }
}
