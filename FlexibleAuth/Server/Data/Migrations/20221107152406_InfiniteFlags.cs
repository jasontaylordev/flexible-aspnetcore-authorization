using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexibleAuth.Server.Data.Migrations
{
    public partial class InfiniteFlags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c70a49b3-dcdf-4db4-8874-2157e8eb4617");

            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<string>(
                name: "Permission",
                table: "AspNetRoles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permission",
                table: "AspNetRoles");

            migrationBuilder.AddColumn<int>(
                name: "Permissions",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Permissions" },
                values: new object[] { "c70a49b3-dcdf-4db4-8874-2157e8eb4617", "98ed9a3b-9bf2-4699-8a64-f7547f46b4d7", "Administrators", "ADMINISTRATORS", 0 });
        }
    }
}
