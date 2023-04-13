using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentierApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class TenantsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantsID",
                table: "RealEstates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_TenantsID",
                table: "RealEstates",
                column: "TenantsID");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Tenants_TenantsID",
                table: "RealEstates",
                column: "TenantsID",
                principalTable: "Tenants",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Tenants_TenantsID",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_TenantsID",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "TenantsID",
                table: "RealEstates");
        }
    }
}
