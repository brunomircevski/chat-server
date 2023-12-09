using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.Migrations
{
    /// <inheritdoc />
    public partial class uuidIndexMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_dateCreated",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_dateCreated_uuid",
                table: "Messages",
                columns: new[] { "dateCreated", "uuid" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Messages_dateCreated_uuid",
                table: "Messages");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_dateCreated",
                table: "Messages",
                column: "dateCreated");
        }
    }
}
