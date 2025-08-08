using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApplication.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddLibrarySerctionAndBooksInSection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LibrarySections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibrarySections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibrarySections_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BooksInSections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LibrarySectionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksInSections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BooksInSections_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksInSections_LibrarySections_LibrarySectionId",
                        column: x => x.LibrarySectionId,
                        principalTable: "LibrarySections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksInSections_BookId",
                table: "BooksInSections",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BooksInSections_LibrarySectionId",
                table: "BooksInSections",
                column: "LibrarySectionId");

            migrationBuilder.CreateIndex(
                name: "IX_LibrarySections_OwnerId",
                table: "LibrarySections",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksInSections");

            migrationBuilder.DropTable(
                name: "LibrarySections");
        }
    }
}
