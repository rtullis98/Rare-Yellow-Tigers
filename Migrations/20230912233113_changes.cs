using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RareYellowTigers.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 9, 12, 18, 31, 12, 989, DateTimeKind.Local).AddTicks(8924));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 9, 12, 18, 31, 12, 989, DateTimeKind.Local).AddTicks(8955));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedOn",
                value: new DateTime(2023, 9, 11, 21, 37, 17, 340, DateTimeKind.Local).AddTicks(2777));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                column: "CreatedOn",
                value: new DateTime(2023, 9, 11, 21, 37, 17, 340, DateTimeKind.Local).AddTicks(2829));
        }
    }
}
