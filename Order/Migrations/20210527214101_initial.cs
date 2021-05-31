using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ordermicroservice.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    PriceAtPointInTime = table.Column<double>(type: "float", nullable: false),
                    OccuredAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "OccuredAt", "PriceAtPointInTime", "ProductID", "Quantity", "Total", "UserID" },
                values: new object[] { 1, new DateTime(2021, 5, 28, 3, 11, 0, 999, DateTimeKind.Local).AddTicks(8937), 10000.0, 1, 1.0, 10000.0, 1 });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderID", "OccuredAt", "PriceAtPointInTime", "ProductID", "Quantity", "Total", "UserID" },
                values: new object[] { 2, new DateTime(2021, 5, 28, 3, 11, 1, 1, DateTimeKind.Local).AddTicks(1852), 20000.0, 2, 1.0, 20000.0, 2 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
