using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryApp.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingShoppingCartModelAndRelationsBetweenModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShoppingCarts",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OwnerID = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    createdOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCarts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ShoppingCarts_AspNetUsers_OwnerID",
                        column: x => x.OwnerID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BooksInShoppingCarts",
                columns: table => new
                {
                    BookID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShoppingCartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BooksInShoppingCarts", x => new { x.BookID, x.ShoppingCartID });
                    table.ForeignKey(
                        name: "FK_BooksInShoppingCarts_Books_BookID",
                        column: x => x.BookID,
                        principalTable: "Books",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BooksInShoppingCarts_ShoppingCarts_ShoppingCartID",
                        column: x => x.ShoppingCartID,
                        principalTable: "ShoppingCarts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BooksInShoppingCarts_ShoppingCartID",
                table: "BooksInShoppingCarts",
                column: "ShoppingCartID");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_OwnerID",
                table: "ShoppingCarts",
                column: "OwnerID",
                unique: true,
                filter: "[OwnerID] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BooksInShoppingCarts");

            migrationBuilder.DropTable(
                name: "ShoppingCarts");
        }
    }
}
