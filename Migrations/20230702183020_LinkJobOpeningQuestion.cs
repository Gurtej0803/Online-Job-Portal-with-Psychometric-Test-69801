using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class LinkJobOpeningQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TestQuestionCategoryId",
                table: "JobOpenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobOpenings_TestQuestionCategoryId",
                table: "JobOpenings",
                column: "TestQuestionCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOpenings_TestQuestionCategory_TestQuestionCategoryId",
                table: "JobOpenings",
                column: "TestQuestionCategoryId",
                principalTable: "TestQuestionCategory",
                principalColumn: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobOpenings_TestQuestionCategory_TestQuestionCategoryId",
                table: "JobOpenings");

            migrationBuilder.DropIndex(
                name: "IX_JobOpenings_TestQuestionCategoryId",
                table: "JobOpenings");

            migrationBuilder.DropColumn(
                name: "TestQuestionCategoryId",
                table: "JobOpenings");
        }
    }
}
