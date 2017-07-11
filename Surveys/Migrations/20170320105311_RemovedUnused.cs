using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Surveys.Migrations
{
    public partial class RemovedUnused : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisibleToSpeakers",
                table: "Surveys");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Surveys");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsVisibleToSpeakers",
                table: "Surveys",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Surveys",
                nullable: false,
                defaultValue: 0);
        }
    }
}
