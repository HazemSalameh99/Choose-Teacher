﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Choose_Teacher.Migrations
{
    /// <inheritdoc />
    public partial class AddPasswordOfTeacher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "Teachers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Password",
                table: "Teachers");
        }
    }
}
