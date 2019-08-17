using Microsoft.EntityFrameworkCore.Migrations;

namespace Iwannago.Api.Migrations
{
    public partial class newindexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_TaxiCabTrips_dropoff_datetime",
                table: "TaxiCabTrips",
                column: "dropoff_datetime");

            migrationBuilder.CreateIndex(
                name: "IX_TaxiCabTrips_pickup_datetime",
                table: "TaxiCabTrips",
                column: "pickup_datetime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TaxiCabTrips_dropoff_datetime",
                table: "TaxiCabTrips");

            migrationBuilder.DropIndex(
                name: "IX_TaxiCabTrips_pickup_datetime",
                table: "TaxiCabTrips");
        }
    }
}
