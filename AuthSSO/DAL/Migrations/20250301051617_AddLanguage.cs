using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Uid",
                keyValue: new Guid("f265c353-e68c-463a-9497-1c4d589a9ae5"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("1537a523-57dd-446e-875b-38f4b175bc85"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("157ffc20-068f-4554-95a3-b9299b523bd1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("30ee8405-0335-458f-8e9a-b60edcf16f52"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("60ae897d-7e13-4180-ad24-86276d1489a2"));

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Applications",
                columns: new[] { "Uid", "DateBreak", "DateLastCheck", "Description", "Ip", "IsActive", "IsWork", "Name", "Port", "Version" },
                values: new object[] { new Guid("1162c098-330f-46f1-ae8a-110625ac0c64"), null, null, "Приложения для списка задач, которые нужно сделать для восстановления Волги", "10.10.10.10", true, true, "Volga-Tracker", 10, "v1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Uid", "ApiKeyUid", "Code", "DateCreate", "DateUpdate", "Email", "FirstName", "HashPassword", "Ip", "IsActive", "IsActiveCode", "IsApiKey", "IsPass", "IsUserPass", "Language", "LastName", "Login", "MiddleName", "RefreshTokenUid", "Salt" },
                values: new object[,]
                {
                    { new Guid("2fbd2424-4f07-4262-a85a-ddad143636b2"), null, "000000", new DateTime(2025, 3, 1, 5, 16, 16, 129, DateTimeKind.Utc).AddTicks(1275), null, "test2@gmail.com", "Степан", null, "127.0.0.1", true, false, false, false, true, 0, "Кондрашов", "Stepan", "Андреевич", null, null },
                    { new Guid("7a1bda3f-9543-4208-aacd-2dd6d07a5d5a"), null, "000000", new DateTime(2025, 3, 1, 5, 16, 16, 128, DateTimeKind.Utc).AddTicks(8792), null, null, "Дмитрий", null, "127.0.0.1", true, false, false, false, true, 0, "Патюков", "Totohka", "Анатольевич", null, null },
                    { new Guid("857c9bd8-6e11-4195-b4a8-fe3b94502856"), null, "000000", new DateTime(2025, 3, 1, 5, 16, 16, 129, DateTimeKind.Utc).AddTicks(1262), null, "test@gmail.com", "Эдуард", null, "127.0.0.1", true, false, false, false, true, 0, "Новиков", "Nedoff", "Дмитриевич", null, null },
                    { new Guid("b2b612b0-0149-4b85-a669-96cd7c4926e8"), null, "000000", new DateTime(2025, 3, 1, 5, 16, 16, 129, DateTimeKind.Utc).AddTicks(1282), null, "test3@gmail.com", "Кирилл", null, "127.0.0.1", true, false, false, false, true, 0, "Шилов", "Kirill", "Александрович", null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Applications",
                keyColumn: "Uid",
                keyValue: new Guid("1162c098-330f-46f1-ae8a-110625ac0c64"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("2fbd2424-4f07-4262-a85a-ddad143636b2"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("7a1bda3f-9543-4208-aacd-2dd6d07a5d5a"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("857c9bd8-6e11-4195-b4a8-fe3b94502856"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Uid",
                keyValue: new Guid("b2b612b0-0149-4b85-a669-96cd7c4926e8"));

            migrationBuilder.DropColumn(
                name: "Language",
                table: "Users");

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
        }
    }
}
