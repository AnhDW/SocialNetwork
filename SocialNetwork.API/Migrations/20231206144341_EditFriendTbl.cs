using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.API.Migrations
{
    /// <inheritdoc />
    public partial class EditFriendTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "Friends",
                newName: "Timestamp");

            migrationBuilder.AddColumn<bool>(
                name: "IsFriend",
                table: "Friends",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFriend",
                table: "Friends");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Friends",
                newName: "CreatedDate");
        }
    }
}
