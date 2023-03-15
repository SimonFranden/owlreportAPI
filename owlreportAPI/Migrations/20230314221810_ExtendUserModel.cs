using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwlreportAPI.Migrations
{
    public partial class ExtendUserModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SecretKey",
                table: "Users",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SecretKey",
                table: "Users");
        }
    }
}
