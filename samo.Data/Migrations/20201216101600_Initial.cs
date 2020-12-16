using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace samo.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoleClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    ProviderKey = table.Column<string>(nullable: true),
                    ProviderDisplayName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 200, nullable: false),
                    LastName = table.Column<string>(maxLength: 200, nullable: false),
                    Phone = table.Column<string>(nullable: true),
                    LimitMoney = table.Column<decimal>(nullable: false),
                    AccountBalance = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "MakeMoney",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Img = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MakeMoney", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spend",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    Img = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spend", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisterMakeMoney",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Money = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IdUser = table.Column<Guid>(nullable: false),
                    IdMakeMoney = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterMakeMoney", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisterMakeMoney_MakeMoney_IdMakeMoney",
                        column: x => x.IdMakeMoney,
                        principalTable: "MakeMoney",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisterMakeMoney_AppUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegisterSpend",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Money = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(maxLength: 200, nullable: false),
                    DateCreate = table.Column<DateTime>(nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    IdUser = table.Column<Guid>(nullable: false),
                    IdSpend = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisterSpend", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisterSpend_Spend_IdSpend",
                        column: x => x.IdSpend,
                        principalTable: "Spend",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegisterSpend_AppUsers_IdUser",
                        column: x => x.IdUser,
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AppRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Description", "Name", "NormalizedName" },
                values: new object[] { new Guid("8d04dce2-969a-435d-bba4-df3f325983dc"), "5f27f2d6-f8f9-4e5e-a3a5-793c44e8f32f", "Administrator role", "admin", "admin" });

            migrationBuilder.InsertData(
                table: "AppUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), new Guid("8d04dce2-969a-435d-bba4-df3f325983dc") });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountBalance", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LimitMoney", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phone", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("69bd714f-9576-45ba-b5b7-f00649be00de"), 0, 0m, "67db1d9e-b614-437a-bab3-cb0f6150259b", "truongnghiahtn@gmail.com", true, "Nghia", "Truong", 100000m, false, null, "truongnghiahtn@gmail.com", "admin", "AQAAAAEAACcQAAAAEOi+N2M+O5xl8v3ry5FDa0NGhaHRpL5hl/jPJvRnZp013eUitLQBwTPowiIUrXsNSA==", null, null, false, "", false, "admin" });

            migrationBuilder.InsertData(
                table: "MakeMoney",
                columns: new[] { "Id", "Description", "Img", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "chiphi", "eating.png", "Ăn uống", true },
                    { 2, "chiphi", "clothes.png", "Quần áo", true },
                    { 3, "chiphi", "fruit.png", "Hoa quả", true },
                    { 4, "chiphi", "traffic.png", "Giao thông", true },
                    { 5, "chiphi", "house.png", "Nhà ở", true }
                });

            migrationBuilder.InsertData(
                table: "Spend",
                columns: new[] { "Id", "Description", "Img", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "thunhap", "lease.png", "Cho thuê", true },
                    { 2, "thunhap", "donation.png", "Quyên góp", true },
                    { 3, "thunhap", "sell.png", "Bán hàng", true },
                    { 4, "thunhap", "devidend.png", "Cổ tức", true },
                    { 5, "thunhap", "refund.png", "Hoàn tiền", true },
                    { 6, "thunhap", "salary.png", "Tiền lương", true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisterMakeMoney_IdMakeMoney",
                table: "RegisterMakeMoney",
                column: "IdMakeMoney");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterMakeMoney_IdUser",
                table: "RegisterMakeMoney",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterSpend_IdSpend",
                table: "RegisterSpend",
                column: "IdSpend");

            migrationBuilder.CreateIndex(
                name: "IX_RegisterSpend_IdUser",
                table: "RegisterSpend",
                column: "IdUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppRoleClaims");

            migrationBuilder.DropTable(
                name: "AppRoles");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserRoles");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "RegisterMakeMoney");

            migrationBuilder.DropTable(
                name: "RegisterSpend");

            migrationBuilder.DropTable(
                name: "MakeMoney");

            migrationBuilder.DropTable(
                name: "Spend");

            migrationBuilder.DropTable(
                name: "AppUsers");
        }
    }
}
