using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearnMUSIC.Migrations
{
    /// <inheritdoc />
    public partial class AddInstrumentAsChildUserClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInstruments_Users_UserId",
                table: "UserInstruments");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInstruments_Users_UserId",
                table: "UserInstruments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserInstruments_Users_UserId",
                table: "UserInstruments");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInstruments_Users_UserId",
                table: "UserInstruments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
