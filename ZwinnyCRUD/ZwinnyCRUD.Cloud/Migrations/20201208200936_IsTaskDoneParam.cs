using Microsoft.EntityFrameworkCore.Migrations;

namespace ZwinnyCRUD.Cloud.Migrations
{
    public partial class IsTaskDoneParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDone",
                table: "Task",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDone",
                table: "Task");
        }
    }
}
