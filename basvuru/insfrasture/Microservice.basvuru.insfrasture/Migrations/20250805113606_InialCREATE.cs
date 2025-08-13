using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Microservice.basvuru.insfrasture.Migrations
{
    /// <inheritdoc />
    public partial class InialCREATE : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Basvurular",
                columns: table => new
                {
                    Basvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriBasvuru_UID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MusteriNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasvuruDurum = table.Column<int>(type: "int", nullable: false),
                    Basvurutipi = table.Column<int>(type: "int", nullable: false),
                    BasvuruTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HataAciklama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kayit_Zaman = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Kayit_Yapan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Kayit_Durum = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Basvurular", x => x.Basvuru_UID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Basvurular");
        }
    }
}
