using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddResume : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<byte[]>(
                name: "Resume",
                table: "JobApplications",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "646a8efc-178c-4b19-b6ac-79c7f24ae566",
                column: "ConcurrencyStamp",
                value: "2f0700f0-6fb2-4a86-b9f6-9fc94373dd26");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3",
                column: "ConcurrencyStamp",
                value: "6c7d06a1-195f-4c88-a829-6b4adc61fbea");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7874ba3-5b86-4106-b582-3f373d23a18c",
                column: "ConcurrencyStamp",
                value: "2dc4b107-4f22-43b1-a551-cc6c20b7c2b9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8e2037-10d0-411f-b978-a5868219f1a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9a761d49-abea-4b1d-8cb4-ea571e250564", "AQAAAAEAACcQAAAAEIOkgfa1o5Wfu4fULJRdHr2FAzcZmSq8AYlmoTF4dY+mBK1wJ7qkHznrZtPa9M//fw==", "9337f277-295e-4da0-92c9-52fbaf58a163" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Resume",
                table: "JobApplications");

            migrationBuilder.AddColumn<Guid>(
                name: "TestQuestionCategoryId",
                table: "JobOpenings",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "646a8efc-178c-4b19-b6ac-79c7f24ae566",
                column: "ConcurrencyStamp",
                value: "7fc65f22-efc1-41c2-8f58-cdcacf2c0e3a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3",
                column: "ConcurrencyStamp",
                value: "20799bcb-affc-4a05-aebd-29f61e9c90f2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7874ba3-5b86-4106-b582-3f373d23a18c",
                column: "ConcurrencyStamp",
                value: "e6ced60b-d30a-4206-a5f9-3dc2ddc60555");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8e2037-10d0-411f-b978-a5868219f1a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ebf9773f-8895-4227-b0ef-ec72bd612073", "AQAAAAEAACcQAAAAEGK7rJBOCdxMffJDRT5tAKI9mRQT99s+sxKFMkajv/+nj/0crxFA0TJuigeNQC1U4A==", "41b35621-43c9-4569-a31c-bbd03ddb403d" });

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
    }
}
