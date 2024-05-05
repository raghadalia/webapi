using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApi.Migrations
{
    public partial class editModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Categories",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "DueDate",
                table: "ToDos");

            migrationBuilder.DropColumn(
                name: "PriorityLevel",
                table: "ToDos");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "451bb74f-4123-43d5-bf4b-a4e044be1e18", "aeee6c7f-8a1c-4051-95b3-7fe38f5d5ad6", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b1131eb9-e50d-42b3-ba4b-be454ae14d5a", "85be6fbd-c5e5-4a1d-9b6b-2cc9091f939d", "Admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "451bb74f-4123-43d5-bf4b-a4e044be1e18");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b1131eb9-e50d-42b3-ba4b-be454ae14d5a");

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "ToDos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DueDate",
                table: "ToDos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PriorityLevel",
                table: "ToDos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
