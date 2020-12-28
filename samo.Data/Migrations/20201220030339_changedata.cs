using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace samo.Data.Migrations
{
    public partial class changedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "RegisterSpend",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "RegisterMakeMoney",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "9369f89a-f118-4b2d-a89d-b1fcbb16c954");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "5d7220ad-2737-4d82-9a70-61fc3a250e22", "AQAAAAEAACcQAAAAEL5fAG5jwBPHa4nz7nNCOFCGGiWcoWXacD/NhxBYkwS0+kV8E12bBIwxtJvHyvn2dQ==" });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "lease.png", "Cho thuê", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "donation.png", "Quyên góp", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "sell.png", "Bán hàng", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "devidend.png", "Cổ tức", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "refund.png", "Hoàn tiền", true });

            migrationBuilder.InsertData(
                table: "MakeMoney",
                columns: new[] { "Id", "Description", "Img", "Name", "Status" },
                values: new object[] { 6, "thunhap", "salary.png", "Tiền lương", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "eating.png", "Ăn uống", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "clothes.png", "Quần áo", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "fruit.png", "Hoa quả", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "traffic.png", "Giao thông", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "house.png", "Nhà ở", true });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "RegisterSpend",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreate",
                table: "RegisterMakeMoney",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime));

            migrationBuilder.UpdateData(
                table: "AppRoles",
                keyColumn: "Id",
                keyValue: new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"),
                column: "ConcurrencyStamp",
                value: "5f27f2d6-f8f9-4e5e-a3a5-793c44e8f32f");

            migrationBuilder.UpdateData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"),
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "67db1d9e-b614-437a-bab3-cb0f6150259b", "AQAAAAEAACcQAAAAEOi+N2M+O5xl8v3ry5FDa0NGhaHRpL5hl/jPJvRnZp013eUitLQBwTPowiIUrXsNSA==" });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "eating.png", "Ăn uống", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "clothes.png", "Quần áo", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "fruit.png", "Hoa quả", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "traffic.png", "Giao thông", true });

            migrationBuilder.UpdateData(
                table: "MakeMoney",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "chiphi", "house.png", "Nhà ở", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "lease.png", "Cho thuê", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "donation.png", "Quyên góp", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "sell.png", "Bán hàng", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "devidend.png", "Cổ tức", true });

            migrationBuilder.UpdateData(
                table: "Spend",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Description", "Img", "Name", "Status" },
                values: new object[] { "thunhap", "refund.png", "Hoàn tiền", true });

            migrationBuilder.InsertData(
                table: "Spend",
                columns: new[] { "Id", "Description", "Img", "Name", "Status" },
                values: new object[] { 6, "thunhap", "salary.png", "Tiền lương", true });
        }
    }
}
