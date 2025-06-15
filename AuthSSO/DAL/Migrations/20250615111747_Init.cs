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
                    TypeApplication = table.Column<int>(type: "integer", nullable: false),
                    DateLastCheck = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateBreak = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Login = table.Column<string>(type: "text", nullable: true),
                    Password = table.Column<string>(type: "text", nullable: true),
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
                    Language = table.Column<int>(type: "integer", nullable: false),
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
                columns: new[] { "Uid", "DateBreak", "DateLastCheck", "Description", "Ip", "IsActive", "IsWork", "Login", "Name", "Password", "Port", "TypeApplication", "Version" },
                values: new object[,]
                {
                    { new Guid("3f597bd4-b9dd-4788-aa02-8db6125ef945"), null, null, "База данных для пользователей", "195.133.28.197", true, true, "postgres", "UserBeer", "3G4rc3093jBlYgEaha/fOw==", 5432, 3, "v1" },
                    { new Guid("4e0129a9-fd5e-4110-95ff-cd19b43d3c72"), null, null, "База данных для волга-трекер", "195.133.28.197", true, true, "postgres", "volga_tracker", "3G4rc3093jBlYgEaha/fOw==", 5432, 3, "v1" },
                    { new Guid("79e32f14-c263-4a2e-9f1c-0185e8c03ce6"), null, null, "Приложения для списка задач, которые нужно сделать для восстановления Волги", "195.133.28.197", true, true, null, "Volga-Tracker", null, 10, 2, "v1" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Uid", "ApiKeyUid", "Code", "DateCreate", "DateUpdate", "Email", "FirstName", "HashPassword", "Ip", "IsActive", "IsActiveCode", "IsApiKey", "IsPass", "IsUserPass", "Language", "LastName", "Login", "MiddleName", "RefreshTokenUid", "Salt" },
                values: new object[,]
                {
                    { new Guid("09814dd3-e028-40ae-81ee-6b16653d57fa"), null, "000000", new DateTime(2025, 6, 15, 11, 17, 46, 115, DateTimeKind.Utc).AddTicks(3115), null, null, "Степан", null, "127.0.0.1", true, false, false, false, true, 0, "Кондрашов", "Stepan", "Андреевич", null, null },
                    { new Guid("4a087b1a-0195-493e-8324-4c26ebc0c4eb"), null, "000000", new DateTime(2025, 6, 15, 11, 17, 46, 115, DateTimeKind.Utc).AddTicks(1159), null, null, "Дмитрий", null, "127.0.0.1", true, false, false, false, true, 0, "Патюков", "Totohka", "Анатольевич", null, null },
                    { new Guid("5436fc04-a7d3-4a45-ad7f-861c1eba0ed0"), null, "000000", new DateTime(2025, 6, 15, 11, 17, 46, 115, DateTimeKind.Utc).AddTicks(3105), null, null, "Эдуард", null, "127.0.0.1", true, false, false, false, true, 0, "Новиков", "Nedoff", "Дмитриевич", null, null },
                    { new Guid("ad63676a-0994-427d-a639-17cb7ccf1fc2"), null, "000000", new DateTime(2025, 6, 15, 11, 17, 46, 115, DateTimeKind.Utc).AddTicks(3120), null, null, "Кирилл", null, "127.0.0.1", true, false, false, false, true, 0, "Шилов", "Kirill", "Александрович", null, null }
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
