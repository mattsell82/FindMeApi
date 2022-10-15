using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FindMeApi.Data.Migrations
{
    public partial class simplifieddatamodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tracks_TrackId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Tracks_TrackId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "Tracks");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TrackId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "TrackId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "TrackId",
                table: "Locations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_TrackId",
                table: "Locations",
                newName: "IX_Locations_UserId");

            migrationBuilder.CreateTable(
                name: "ApplicationUserApplicationUser",
                columns: table => new
                {
                    FollowersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FollowingId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserApplicationUser", x => new { x.FollowersId, x.FollowingId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUser_AspNetUsers_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserApplicationUser_AspNetUsers_FollowingId",
                        column: x => x.FollowingId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserApplicationUser_FollowingId",
                table: "ApplicationUserApplicationUser",
                column: "FollowingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_AspNetUsers_UserId",
                table: "Locations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_AspNetUsers_UserId",
                table: "Locations");

            migrationBuilder.DropTable(
                name: "ApplicationUserApplicationUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Locations",
                newName: "TrackId");

            migrationBuilder.RenameIndex(
                name: "IX_Locations_UserId",
                table: "Locations",
                newName: "IX_Locations_TrackId");

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

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TrackId",
                table: "AspNetUsers",
                column: "TrackId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tracks_TrackId",
                table: "AspNetUsers",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Tracks_TrackId",
                table: "Locations",
                column: "TrackId",
                principalTable: "Tracks",
                principalColumn: "Id");
        }
    }
}
