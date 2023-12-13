using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.API.Migrations
{
    /// <inheritdoc />
    public partial class dropColumnUserIdInAttachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Attachments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
