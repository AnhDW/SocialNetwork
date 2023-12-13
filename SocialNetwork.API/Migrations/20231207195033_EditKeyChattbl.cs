using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.API.Migrations
{
    /// <inheritdoc />
    public partial class EditKeyChattbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Chats",
                newName: "Timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                columns: new[] { "SenderId", "ReceiverId", "Timestamp" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Chats",
                newName: "CreatedDate");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                columns: new[] { "SenderId", "ReceiverId" });
        }
    }
}
