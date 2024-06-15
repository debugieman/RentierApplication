using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentierApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class tenants_collection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_AspNetUsers_UserId",
                table: "RealEstates");

            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Tenants_TenantsID",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_TenantsID",
                table: "RealEstates");

            migrationBuilder.DropIndex(
                name: "IX_RealEstates_UserId",
                table: "RealEstates");

            migrationBuilder.DropColumn(
                name: "TenantsID",
                table: "RealEstates");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RealEstates",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "RealEstates",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TenantsID",
                table: "RealEstates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_TenantsID",
                table: "RealEstates",
                column: "TenantsID");

            migrationBuilder.CreateIndex(
                name: "IX_RealEstates_UserId",
                table: "RealEstates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_AspNetUsers_UserId",
                table: "RealEstates",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Tenants_TenantsID",
                table: "RealEstates",
                column: "TenantsID",
                principalTable: "Tenants",
                principalColumn: "ID");
        }
    }
}
