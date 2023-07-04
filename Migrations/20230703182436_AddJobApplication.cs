using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddJobApplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobApplications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    JobOpeningJobId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicationStatus = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobApplications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobApplications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobApplications_JobOpenings_JobOpeningJobId",
                        column: x => x.JobOpeningJobId,
                        principalTable: "JobOpenings",
                        principalColumn: "JobId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TestResults",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TestCategoryCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Score = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResults", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TestResults_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TestResults_TestQuestionCategory_TestCategoryCategoryId",
                        column: x => x.TestCategoryCategoryId,
                        principalTable: "TestQuestionCategory",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_JobApplications_JobOpeningJobId",
                table: "JobApplications",
                column: "JobOpeningJobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobApplications_UserId",
                table: "JobApplications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_TestCategoryCategoryId",
                table: "TestResults",
                column: "TestCategoryCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TestResults_UserId",
                table: "TestResults",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobApplications");

            migrationBuilder.DropTable(
                name: "TestResults");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "646a8efc-178c-4b19-b6ac-79c7f24ae566",
                column: "ConcurrencyStamp",
                value: "31270c05-9514-421d-8a2f-cf50d29352f6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3",
                column: "ConcurrencyStamp",
                value: "9b4fd819-8ecd-4693-a5b6-01463fd940de");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7874ba3-5b86-4106-b582-3f373d23a18c",
                column: "ConcurrencyStamp",
                value: "61b696c6-4bc9-422d-bb3d-9941d1b8bb52");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8e2037-10d0-411f-b978-a5868219f1a8",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2dc87431-ea87-4602-8e52-46c2596f01c0", "AQAAAAEAACcQAAAAEP6CZ9aCI5ODhKQzO/Joz3bgqWxbiYx7HoXPGJH9kliakOgtBzsqYg0LaANIJ0fwNA==", "9bbe8262-f4fc-4826-a9dc-d74783c9ed3b" });
        }
    }
}
