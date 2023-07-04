using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class RemovePictureColumnsfromTestQuestionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OptionAPicture",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "OptionBPicture",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "OptionCPicture",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "OptionDPicture",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "QuestionPicture",
                table: "TestQuestion");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "OptionAPicture",
                table: "TestQuestion",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "OptionBPicture",
                table: "TestQuestion",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "OptionCPicture",
                table: "TestQuestion",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "OptionDPicture",
                table: "TestQuestion",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "QuestionPicture",
                table: "TestQuestion",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
