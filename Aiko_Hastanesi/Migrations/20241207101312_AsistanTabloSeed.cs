using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aiko_Hastanesi.Migrations
{
    /// <inheritdoc />
    public partial class AsistanTabloSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Asistans",
                columns: new[] { "Id", "AdSoyad", "Adres", "Email", "Telefon" },
                values: new object[,]
                {
                    { 1, "Ali Yılmaz", "İstanbul, Türkiye", "ali.yilmaz@example.com", "1234567890" },
                    { 2, "Ayşe Demir", "Ankara, Türkiye", "ayse.demir@example.com", "0987654321" },
                    { 3, "Mehmet Can", "İzmir, Türkiye", "mehmet.can@example.com", "5551234567" },
                    { 4, "Fatma Korkmaz", "Bursa, Türkiye", "fatma.korkmaz@example.com", "4445678901" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Asistans",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Asistans",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Asistans",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Asistans",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
