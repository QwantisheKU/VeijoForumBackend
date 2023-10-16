using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeijoForumBackend.Migrations
{
    /// <inheritdoc />
    public partial class listoftopics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Topic_CategoryId",
                table: "Topic");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_CategoryId",
                table: "Topic",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Topic_CategoryId",
                table: "Topic");

            migrationBuilder.CreateIndex(
                name: "IX_Topic_CategoryId",
                table: "Topic",
                column: "CategoryId",
                unique: true);
        }
    }
}
