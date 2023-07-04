using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddImagePathColumnsToTestQuestionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePathOptionA",
                table: "TestQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePathOptionB",
                table: "TestQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePathOptionC",
                table: "TestQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePathOptionD",
                table: "TestQuestion",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePathQuestion",
                table: "TestQuestion",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePathOptionA",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "ImagePathOptionB",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "ImagePathOptionC",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "ImagePathOptionD",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "ImagePathQuestion",
                table: "TestQuestion");
        }
    }
}
