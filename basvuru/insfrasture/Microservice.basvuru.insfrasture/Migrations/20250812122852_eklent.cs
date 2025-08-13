using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.basvuru.insfrasture.Migrations
{
    /// <inheritdoc />
    public partial class eklent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MusteriBasvuru_UID",
                table: "Basvurular",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "MusteriBasvuru_UID",
                table: "Basvurular",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
