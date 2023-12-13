using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetwork.API.Migrations
{
    /// <inheritdoc />
    public partial class AddChatRoomTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_Users_CreatorId",
                table: "Room");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomMember_Room_RoomId",
                table: "RoomMember");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomMember_Users_MemberId",
                table: "RoomMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomMember",
                table: "RoomMember");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Room",
                table: "Room");

            migrationBuilder.RenameTable(
                name: "RoomMember",
                newName: "RoomMembers");

            migrationBuilder.RenameTable(
                name: "Room",
                newName: "Rooms");

            migrationBuilder.RenameIndex(
                name: "IX_RoomMember_MemberId",
                table: "RoomMembers",
                newName: "IX_RoomMembers_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Room_CreatorId",
                table: "Rooms",
                newName: "IX_Rooms_CreatorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomMembers",
                table: "RoomMembers",
                columns: new[] { "RoomId", "MemberId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ChatRooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatRooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChatRooms_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChatRooms_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_RoomId",
                table: "ChatRooms",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_ChatRooms_UserId",
                table: "ChatRooms",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMembers_Rooms_RoomId",
                table: "RoomMembers",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMembers_Users_MemberId",
                table: "RoomMembers",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Users_CreatorId",
                table: "Rooms",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomMembers_Rooms_RoomId",
                table: "RoomMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomMembers_Users_MemberId",
                table: "RoomMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Users_CreatorId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "ChatRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rooms",
                table: "Rooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomMembers",
                table: "RoomMembers");

            migrationBuilder.RenameTable(
                name: "Rooms",
                newName: "Room");

            migrationBuilder.RenameTable(
                name: "RoomMembers",
                newName: "RoomMember");

            migrationBuilder.RenameIndex(
                name: "IX_Rooms_CreatorId",
                table: "Room",
                newName: "IX_Room_CreatorId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomMembers_MemberId",
                table: "RoomMember",
                newName: "IX_RoomMember_MemberId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Room",
                table: "Room",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomMember",
                table: "RoomMember",
                columns: new[] { "RoomId", "MemberId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Room_Users_CreatorId",
                table: "Room",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMember_Room_RoomId",
                table: "RoomMember",
                column: "RoomId",
                principalTable: "Room",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomMember_Users_MemberId",
                table: "RoomMember",
                column: "MemberId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
