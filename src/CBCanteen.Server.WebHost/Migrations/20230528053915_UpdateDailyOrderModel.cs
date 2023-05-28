﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBCanteen.Server.WebHost.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDailyOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfConsumption",
                table: "DailyOrders",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfConsumption",
                table: "DailyOrders");
        }
    }
}
