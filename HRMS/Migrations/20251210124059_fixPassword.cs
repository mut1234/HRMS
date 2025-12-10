using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class fixPassword : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HashedPassword",
                value: "$2a$11$oRZUJZIpM0sRP855lUEEZe5JZ4CAL1UNGpFjEj6lSGx.wgbZ/na8W");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L,
                column: "HashedPassword",
                value: "$2a$11$58OInCwBT3xDeI/uFShdau1oYw83SXE9WkONDVBfh/uWwtRt89lwO");
        }
    }
}
