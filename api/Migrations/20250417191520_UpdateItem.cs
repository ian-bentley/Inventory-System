using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_EventTypes_CurrentEventTypeId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CurrentEventTypeId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CurrentEventTypeId",
                table: "Items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrentEventTypeId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_CurrentEventTypeId",
                table: "Items",
                column: "CurrentEventTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_EventTypes_CurrentEventTypeId",
                table: "Items",
                column: "CurrentEventTypeId",
                principalTable: "EventTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
