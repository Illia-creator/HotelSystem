using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangedCardNumberStyle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Resevations_ReservationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Resevations_Rooms_RoomId",
                table: "Resevations");

            migrationBuilder.DropForeignKey(
                name: "FK_Resevations_Users_UserId",
                table: "Resevations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Resevations",
                table: "Resevations");

            migrationBuilder.RenameTable(
                name: "Resevations",
                newName: "Reservations");

            migrationBuilder.RenameIndex(
                name: "IX_Resevations_UserId",
                table: "Reservations",
                newName: "IX_Reservations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Resevations_RoomId",
                table: "Reservations",
                newName: "IX_Reservations_RoomId");

            migrationBuilder.AlterColumn<string>(
                name: "CardNumber",
                table: "Payments",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments",
                column: "ReservationId",
                principalTable: "Reservations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Reservations_ReservationId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Rooms_RoomId",
                table: "Reservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Users_UserId",
                table: "Reservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reservations",
                table: "Reservations");

            migrationBuilder.RenameTable(
                name: "Reservations",
                newName: "Resevations");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_UserId",
                table: "Resevations",
                newName: "IX_Resevations_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reservations_RoomId",
                table: "Resevations",
                newName: "IX_Resevations_RoomId");

            migrationBuilder.AlterColumn<int>(
                name: "CardNumber",
                table: "Payments",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Resevations",
                table: "Resevations",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Resevations_ReservationId",
                table: "Payments",
                column: "ReservationId",
                principalTable: "Resevations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resevations_Rooms_RoomId",
                table: "Resevations",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Resevations_Users_UserId",
                table: "Resevations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
