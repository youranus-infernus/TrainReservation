using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainReservation.Migrations
{
    public partial class AddSelectedStationsToTrip : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SelectedStationFromId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SelectedStationToId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SelectedStationFromId",
                table: "Trips",
                column: "SelectedStationFromId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_SelectedStationToId",
                table: "Trips",
                column: "SelectedStationToId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_SelectedStationFromId",
                table: "Trips",
                column: "SelectedStationFromId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Stations_SelectedStationToId",
                table: "Trips",
                column: "SelectedStationToId",
                principalTable: "Stations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_SelectedStationFromId",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Stations_SelectedStationToId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_SelectedStationFromId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_SelectedStationToId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "SelectedStationFromId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "SelectedStationToId",
                table: "Trips");
        }
    }
}
