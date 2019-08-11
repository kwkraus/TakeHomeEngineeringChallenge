using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Iwannago.Api.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaxiCabTrips",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TaxiType = table.Column<string>(nullable: true),
                    VendorID = table.Column<string>(nullable: true),
                    pickup_datetime = table.Column<DateTime>(nullable: false),
                    dropoff_datetime = table.Column<DateTime>(nullable: false),
                    store_and_fwd_flag = table.Column<string>(nullable: true),
                    RatecodeID = table.Column<int>(nullable: false),
                    PULocationID = table.Column<int>(nullable: false),
                    DOLocationID = table.Column<int>(nullable: false),
                    passenger_count = table.Column<int>(nullable: false),
                    trip_distance = table.Column<decimal>(nullable: false),
                    fare_amount = table.Column<decimal>(nullable: false),
                    extra = table.Column<decimal>(nullable: false),
                    mta_tax = table.Column<decimal>(nullable: false),
                    tip_amount = table.Column<decimal>(nullable: false),
                    tolls_amount = table.Column<decimal>(nullable: false),
                    ehail_fee = table.Column<string>(nullable: true),
                    improvement_surcharge = table.Column<decimal>(nullable: false),
                    total_amount = table.Column<decimal>(nullable: false),
                    payment_type = table.Column<int>(nullable: false),
                    trip_type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaxiCabTrips", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaxiCabTrips_TaxiType",
                table: "TaxiCabTrips",
                column: "TaxiType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaxiCabTrips");
        }
    }
}
