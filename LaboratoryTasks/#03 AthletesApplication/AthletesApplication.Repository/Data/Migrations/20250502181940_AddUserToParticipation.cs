using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AthletesApplication.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddUserToParticipation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Participations",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Participations_OwnerId",
                table: "Participations",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Participations_AspNetUsers_OwnerId",
                table: "Participations",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participations_AspNetUsers_OwnerId",
                table: "Participations");

            migrationBuilder.DropIndex(
                name: "IX_Participations_OwnerId",
                table: "Participations");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Participations");
        }
    }
}
