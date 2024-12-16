using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Aiko_Hastanesi.Migrations
{
    /// <inheritdoc />
    public partial class HocaTabloOluştur : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Hocas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdSoyad = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefon = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hocas", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Hocas",
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
            migrationBuilder.DropTable(
                name: "Hocas");
        }
    }
}
