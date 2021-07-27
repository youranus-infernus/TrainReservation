using Microsoft.EntityFrameworkCore.Migrations;

namespace TrainReservation.Migrations
{
    public partial class AddNameAndSurnameToReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PassengerName",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PassengerSurname",
                table: "Reservations",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PassengerName",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "PassengerSurname",
                table: "Reservations");
        }
    }
}
