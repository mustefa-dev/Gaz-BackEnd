using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GazBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class udatedateofcanceled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfRejected",
                table: "Orders",
                newName: "DateOfCanceled");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateOfCanceled",
                table: "Orders",
                newName: "DateOfRejected");
        }
    }
}
