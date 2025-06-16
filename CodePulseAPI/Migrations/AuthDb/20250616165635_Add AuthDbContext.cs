using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CodePulseAPI.Migrations.AuthDb
{
    /// <inheritdoc />
    public partial class AddAuthDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa4c6ff8-71a8-4bb4-8719-480c45f16bef",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "51c84e2e-b9c4-496e-beb8-f6a2606f38b1", "admin@mail.com", "ADMIN@MAIL.COM", "AQAAAAIAAYagAAAAEOPYektD+a31rDnd1i+anF9ACQBzLiC1RWgJMD1F/Xg93d3LmrqsyYAPxL7zTvXAoA==", "adce481f-05bf-40ba-a2d2-666b9a162154" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "aa4c6ff8-71a8-4bb4-8719-480c45f16bef",
                columns: new[] { "ConcurrencyStamp", "Email", "NormalizedEmail", "PasswordHash", "SecurityStamp" },
                values: new object[] { "501f9000-7a8b-48c8-a2f4-042b5abeaab7", "admin@mail.ru", "ADMIN@MAIL.RU", "AQAAAAIAAYagAAAAECxJneceQTrdHl+OGDleVks8L8Eo4jzgtA8M++0cBrsws41bcgaUd8VT0quHoLKDzA==", "8586fe60-7fd9-4fdc-aaba-5d50bc78d655" });
        }
    }
}
