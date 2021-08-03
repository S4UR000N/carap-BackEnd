using Microsoft.EntityFrameworkCore.Migrations;

namespace api_storm.Migrations
{
    public partial class RIP : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TypeName",
                table: "VehicleTypeModel",
                newName: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "VehicleTypeModel",
                newName: "TypeName");
        }
    }
}
