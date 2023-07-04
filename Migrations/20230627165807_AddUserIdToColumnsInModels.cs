using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddUserIdToColumnsInModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TestQuestionCategory",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TestQuestion",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "JobOpenings",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestionCategory_UserId",
                table: "TestQuestionCategory",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestQuestion_UserId",
                table: "TestQuestion",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_UserId",
                table: "JobOpenings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpenings_AspNetUsers_UserId",
                table: "JobOpenings",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestion_AspNetUsers_UserId",
                table: "TestQuestion",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_TestQuestionCategory_AspNetUsers_UserId",
                table: "TestQuestionCategory",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpenings_AspNetUsers_UserId",
                table: "JobOpenings");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestion_AspNetUsers_UserId",
                table: "TestQuestion");

            migrationBuilder.DropForeignKey(
                name: "FK_TestQuestionCategory_AspNetUsers_UserId",
                table: "TestQuestionCategory");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestionCategory_UserId",
                table: "TestQuestionCategory");

            migrationBuilder.DropIndex(
                name: "IX_TestQuestion_UserId",
                table: "TestQuestion");

            migrationBuilder.DropIndex(
                name: "IX_JobOpenings_UserId",
                table: "JobOpenings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestQuestionCategory");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TestQuestion");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "JobOpenings");
        }
    }
}
