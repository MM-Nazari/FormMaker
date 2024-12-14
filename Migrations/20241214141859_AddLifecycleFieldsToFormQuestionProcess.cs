using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FormMaker.Migrations
{
    /// <inheritdoc />
    public partial class AddLifecycleFieldsToFormQuestionProcess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FormQuestionProcesses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FormQuestionProcesses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FormQuestionProcesses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FormQuestionProcesses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FormQuestionProcesses");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FormQuestionProcesses");
        }
    }
}
