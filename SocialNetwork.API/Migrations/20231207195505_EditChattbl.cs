using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.API.Migrations
{
    /// <inheritdoc />
    public partial class EditChattbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "Chats",
                newName: "LastActive");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Chats",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Chats",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Chats_SenderId",
                table: "Chats",
                column: "SenderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Chats",
                table: "Chats");

            migrationBuilder.DropIndex(
                name: "IX_Chats_SenderId",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Chats");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "LastActive",
                table: "Chats",
                newName: "Timestamp");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Chats",
                table: "Chats",
                columns: new[] { "SenderId", "ReceiverId", "Timestamp" });
        }
    }
}
