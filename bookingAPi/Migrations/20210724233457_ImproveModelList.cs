using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace bookingAPi.Migrations
{
    public partial class ImproveModelList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Cancelled",
                table: "Booking",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelledTimeStamp",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedTimeStamp",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Booking",
                type: "TEXT",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cancelled",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CancelledTimeStamp",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "CreatedTimeStamp",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Booking");
        }
    }
}
