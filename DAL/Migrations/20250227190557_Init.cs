using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiKeys",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateExpire = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiKeys", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Version = table.Column<string>(type: "text", nullable: true),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    Port = table.Column<int>(type: "integer", nullable: false),
                    IsWork = table.Column<bool>(type: "boolean", nullable: false),
                    DateLastCheck = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateBreak = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateExpire = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "WhiteIps",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    ApiKeyUid = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WhiteIps", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_WhiteIps_ApiKeys_ApiKeyUid",
                        column: x => x.ApiKeyUid,
                        principalTable: "ApiKeys",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    ApplicationUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Roles_Applications_ApplicationUid",
                        column: x => x.ApplicationUid,
                        principalTable: "Applications",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: true),
                    MiddleName = table.Column<string>(type: "text", nullable: true),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Ip = table.Column<string>(type: "text", nullable: false),
                    HashPassword = table.Column<string>(type: "text", nullable: true),
                    Salt = table.Column<byte[]>(type: "bytea", nullable: true),
                    IsUserPass = table.Column<bool>(type: "boolean", nullable: false),
                    IsApiKey = table.Column<bool>(type: "boolean", nullable: false),
                    IsPass = table.Column<bool>(type: "boolean", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: false),
                    IsActiveCode = table.Column<bool>(type: "boolean", nullable: false),
                    DateCreate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApiKeyUid = table.Column<Guid>(type: "uuid", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    RefreshTokenUid = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_Users_ApiKeys_ApiKeyUid",
                        column: x => x.ApiKeyUid,
                        principalTable: "ApiKeys",
                        principalColumn: "Uid");
                    table.ForeignKey(
                        name: "FK_Users_RefreshTokens_RefreshTokenUid",
                        column: x => x.RefreshTokenUid,
                        principalTable: "RefreshTokens",
                        principalColumn: "Uid");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Uid = table.Column<Guid>(type: "uuid", nullable: false),
                    UserUid = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleUid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleUid",
                        column: x => x.RoleUid,
                        principalTable: "Roles",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserUid",
                        column: x => x.UserUid,
                        principalTable: "Users",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Uid", "DateBreak", "DateLastCheck", "Description", "Ip", "IsActive", "IsWork", "Name", "Port", "Version" },
                values: new object[] { new Guid("f265c353-e68c-463a-9497-1c4d589a9ae5"), null, null, "Приложения для списка задач, которые нужно сделать для восстановления Волги", "10.10.10.10", true, true, "Volga-Tracker", 10, "v1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Uid", "ApiKeyUid", "Code", "DateCreate", "DateUpdate", "Email", "FirstName", "HashPassword", "Ip", "IsActive", "IsActiveCode", "IsApiKey", "IsPass", "IsUserPass", "LastName", "Login", "MiddleName", "RefreshTokenUid", "Salt" },
                values: new object[,]
                {
                    { new Guid("1537a523-57dd-446e-875b-38f4b175bc85"), null, "000000", new DateTime(2025, 2, 27, 19, 5, 56, 571, DateTimeKind.Utc).AddTicks(5628), null, "test@gmail.com", "Эдуард", null, "127.0.0.1", true, false, false, false, true, "Новиков", "Nedoff", "Дмитриевич", null, null },
                    { new Guid("157ffc20-068f-4554-95a3-b9299b523bd1"), null, "000000", new DateTime(2025, 2, 27, 19, 5, 56, 571, DateTimeKind.Utc).AddTicks(2899), null, null, "Дмитрий", null, "127.0.0.1", true, false, false, false, true, "Патюков", "Totohka", "Анатольевич", null, null },
                    { new Guid("30ee8405-0335-458f-8e9a-b60edcf16f52"), null, "000000", new DateTime(2025, 2, 27, 19, 5, 56, 571, DateTimeKind.Utc).AddTicks(5654), null, "test2@gmail.com", "Степан", null, "127.0.0.1", true, false, false, false, true, "Кондрашов", "Stepan", "Андреевич", null, null },
                    { new Guid("60ae897d-7e13-4180-ad24-86276d1489a2"), null, "000000", new DateTime(2025, 2, 27, 19, 5, 56, 571, DateTimeKind.Utc).AddTicks(5660), null, "test3@gmail.com", "Кирилл", null, "127.0.0.1", true, false, false, false, true, "Шилов", "Kirill", "Александрович", null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Roles_ApplicationUid",
                table: "Roles",
                column: "ApplicationUid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleUid",
                table: "UserRoles",
                column: "RoleUid");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserUid",
                table: "UserRoles",
                column: "UserUid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_ApiKeyUid",
                table: "Users",
                column: "ApiKeyUid");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RefreshTokenUid",
                table: "Users",
                column: "RefreshTokenUid",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WhiteIps_ApiKeyUid",
                table: "WhiteIps",
                column: "ApiKeyUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "WhiteIps");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "ApiKeys");

            migrationBuilder.DropTable(
                name: "RefreshTokens");
        }
    }
}
