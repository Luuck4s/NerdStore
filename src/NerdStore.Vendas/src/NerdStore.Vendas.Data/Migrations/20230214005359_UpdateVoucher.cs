using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NerdStore.Vendas.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVoucher : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrder_Orders_OrderId",
                table: "ItemsOrder");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrder_Orders_OrderId",
                table: "ItemsOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemsOrder_Orders_OrderId",
                table: "ItemsOrder");

            migrationBuilder.AlterColumn<Guid>(
                name: "VoucherId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemsOrder_Orders_OrderId",
                table: "ItemsOrder",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
