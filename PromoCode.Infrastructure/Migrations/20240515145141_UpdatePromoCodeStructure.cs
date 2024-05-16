using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PromoCode.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePromoCodeStructure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "PromotionalCodes",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PromotionalCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "TenantId",
                table: "PromotionalCodes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "PromotionalCodes",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PromotionalCodes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PromotionalCodes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PromotionalCodes");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "PromotionalCodes",
                newName: "IsActive");
        }
    }
}
