using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetingRooms.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_generate_createdat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Reserves",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Reserves");
        }
    }
}
