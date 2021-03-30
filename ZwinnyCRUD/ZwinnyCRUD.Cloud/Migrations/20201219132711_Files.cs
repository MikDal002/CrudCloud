using Microsoft.EntityFrameworkCore.Migrations;

namespace ZwinnyCRUD.Cloud.Migrations
{
    public partial class Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "File",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
