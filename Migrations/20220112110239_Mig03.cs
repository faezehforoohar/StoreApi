using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreApi.Migrations
{
    public partial class Mig03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "YearModel",
                table: "PriceListDs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "YearModel1",
                table: "PriceListDs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearModel1",
                table: "PriceListDs");

            migrationBuilder.AlterColumn<int>(
                name: "YearModel",
                table: "PriceListDs",
                type: "int",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
