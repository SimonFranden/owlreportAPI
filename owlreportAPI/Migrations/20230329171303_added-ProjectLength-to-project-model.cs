using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwlreportAPI.Migrations
{
    public partial class addedProjectLengthtoprojectmodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProjectLength",
                table: "Projects",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectLength",
                table: "Projects");
        }
    }
}
