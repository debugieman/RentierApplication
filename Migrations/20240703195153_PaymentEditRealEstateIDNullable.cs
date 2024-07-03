using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentierApplication.Migrations
{
    /// <inheritdoc />
    public partial class PaymentEditRealEstateIDNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Payments_PaymentId",
                table: "RealEstates");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "RealEstates",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Payments_PaymentId",
                table: "RealEstates",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RealEstates_Payments_PaymentId",
                table: "RealEstates");

            migrationBuilder.AlterColumn<int>(
                name: "PaymentId",
                table: "RealEstates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RealEstates_Payments_PaymentId",
                table: "RealEstates",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
