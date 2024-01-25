using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCProje.Data.Migrations
{
    /// <inheritdoc />
    public partial class tablo_update_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OyunId",
                table: "SepetToplams");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OyunId",
                table: "SepetToplams",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
