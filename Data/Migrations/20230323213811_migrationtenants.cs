using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentierApplication.Data.Migrations
{
    /// <inheritdoc />
    public partial class migrationtenants : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoneyObligation = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Surety = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    RealEstateID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tenants_RealEstates_RealEstateID",
                        column: x => x.RealEstateID,
                        principalTable: "RealEstates",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_RealEstateID",
                table: "Tenants",
                column: "RealEstateID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tenants");
        }
    }
}
