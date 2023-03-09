using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OwlreportAPI.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeReports",
                table: "TimeReports");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "TimeReports",
                newName: "ProjectId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TimeReports",
                newName: "UserName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TimeReports",
                newName: "HoursWorked");

            migrationBuilder.AlterColumn<int>(
                name: "HoursWorked",
                table: "TimeReports",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "TimeReportId",
                table: "TimeReports",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "TimeReports",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeReports",
                table: "TimeReports",
                column: "TimeReportId");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TimeReports",
                table: "TimeReports");

            migrationBuilder.DropColumn(
                name: "TimeReportId",
                table: "TimeReports");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "TimeReports");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "TimeReports",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProjectId",
                table: "TimeReports",
                newName: "Time");

            migrationBuilder.RenameColumn(
                name: "HoursWorked",
                table: "TimeReports",
                newName: "Id");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "TimeReports",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TimeReports",
                table: "TimeReports",
                column: "Id");
        }
    }
}
