using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeatureFlags.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixEnvDto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EnvironmetId",
                table: "Rules");

            migrationBuilder.CreateIndex(
                name: "IX_Rules_Id",
                table: "Rules",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Rules_Id",
                table: "Rules");

            migrationBuilder.AddColumn<Guid>(
                name: "EnvironmetId",
                table: "Rules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
