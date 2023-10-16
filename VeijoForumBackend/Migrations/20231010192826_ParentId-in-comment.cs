using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VeijoForumBackend.Migrations
{
    /// <inheritdoc />
    public partial class ParentIdincomment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ParentId",
                table: "Comment",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ParentId",
                table: "Comment");
        }
    }
}
