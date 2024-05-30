using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GazBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class editorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfAccepted",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfDelivered",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfRejected",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfAccepted",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateOfDelivered",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DateOfRejected",
                table: "Orders");
        }
    }
}
