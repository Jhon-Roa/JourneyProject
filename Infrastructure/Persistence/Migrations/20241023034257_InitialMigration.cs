using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "flights",
                columns: table => new
                {
                    flight_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flight_carrier = table.Column<string>(type: "character varying(2)", maxLength: 2, nullable: false),
                    flight_number = table.Column<string>(type: "character varying(4)", maxLength: 4, nullable: false),
                    origin = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    destination = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_flights", x => x.flight_id);
                });

            migrationBuilder.CreateTable(
                name: "journeys",
                columns: table => new
                {
                    journey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    origin = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    destination = table.Column<string>(type: "character varying(3)", maxLength: 3, nullable: false),
                    Price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journeys", x => x.journey_id);
                });

            migrationBuilder.CreateTable(
                name: "journeys_flights",
                columns: table => new
                {
                    journey_id = table.Column<Guid>(type: "uuid", nullable: false),
                    flight_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_journeys_flights", x => new { x.journey_id, x.flight_id });
                    table.ForeignKey(
                        name: "FK_journeys_flights_flights_flight_id",
                        column: x => x.flight_id,
                        principalTable: "flights",
                        principalColumn: "flight_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_journeys_flights_journeys_journey_id",
                        column: x => x.journey_id,
                        principalTable: "journeys",
                        principalColumn: "journey_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_journeys_flights_flight_id",
                table: "journeys_flights",
                column: "flight_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "journeys_flights");

            migrationBuilder.DropTable(
                name: "flights");

            migrationBuilder.DropTable(
                name: "journeys");
        }
    }
}
