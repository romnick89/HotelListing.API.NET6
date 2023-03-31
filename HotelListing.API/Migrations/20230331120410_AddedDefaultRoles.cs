using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelListing.API.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "67653135-c2f4-4430-9008-2f8af09d0d17", "5d5f0e51-5313-4be6-b6f2-45195c63041c", "Administrator", "ADMINISTRATOR" },
                    { "bfa68b0b-0544-4d2e-972f-fb59c01a72f5", "aed2ebe6-3073-4406-b243-c0f6691753de", "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "67653135-c2f4-4430-9008-2f8af09d0d17");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfa68b0b-0544-4d2e-972f-fb59c01a72f5");
        }
    }
}
