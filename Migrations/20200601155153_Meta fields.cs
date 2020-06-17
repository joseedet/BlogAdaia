﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogAdaia.Migrations
{
    public partial class Metafields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tags",
                table: "Posts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "Posts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "description",
                table: "Posts");
        }
    }
}
