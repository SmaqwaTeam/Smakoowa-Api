using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Smakoowa_Api.Migrations.BackgroundData
{
    /// <inheritdoc />
    public partial class requestCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequestCounts",
                columns: table => new
                {
                    ControllerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RemainingPath = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    Count = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestCounts", x => new { x.ControllerName, x.ActionName, x.RemainingPath });
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RequestCounts");
        }
    }
}
