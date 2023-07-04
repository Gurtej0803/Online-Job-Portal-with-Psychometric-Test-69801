using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobPortal.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "646a8efc-178c-4b19-b6ac-79c7f24ae566", "31270c05-9514-421d-8a2f-cf50d29352f6", "Job Recruiter", "JOB RECRUITER" },
                    { "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3", "9b4fd819-8ecd-4693-a5b6-01463fd940de", "Admin", "ADMIN" },
                    { "f7874ba3-5b86-4106-b582-3f373d23a18c", "61b696c6-4bc9-422d-bb3d-9941d1b8bb52", "Job Seeker", "JOB SEEKER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "CompanyIndustry", "CompanyLocation", "CompanyName", "CompanyType", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "JobSeekerDateofBirth", "JobSeekerGender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePicture", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1d8e2037-10d0-411f-b978-a5868219f1a8", 0, "", "", "", "", "2dc87431-ea87-4602-8e52-46c2596f01c0", "admin@example.com", false, "Admin", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "", false, null, null, null, "AQAAAAEAACcQAAAAEP6CZ9aCI5ODhKQzO/Joz3bgqWxbiYx7HoXPGJH9kliakOgtBzsqYg0LaANIJ0fwNA==", null, false, new byte[0], "9bbe8262-f4fc-4826-a9dc-d74783c9ed3b", false, null });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3", "1d8e2037-10d0-411f-b978-a5868219f1a8" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "646a8efc-178c-4b19-b6ac-79c7f24ae566");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7874ba3-5b86-4106-b582-3f373d23a18c");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3", "1d8e2037-10d0-411f-b978-a5868219f1a8" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba7ad5c8-3ff8-4cb5-981d-9e8e44492dd3");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1d8e2037-10d0-411f-b978-a5868219f1a8");
        }
    }
}
