CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE "ApiKeys" (
    "Uid" uuid NOT NULL,
    "Name" text NOT NULL,
    "Value" text NOT NULL,
    "DateCreate" timestamp with time zone NOT NULL,
    "DateExpire" timestamp with time zone,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_ApiKeys" PRIMARY KEY ("Uid")
);

CREATE TABLE "Applications" (
    "Uid" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "Version" text,
    "Ip" text NOT NULL,
    "Port" integer NOT NULL,
    "IsWork" boolean NOT NULL,
    "TypeApplication" integer NOT NULL,
    "DateLastCheck" timestamp with time zone,
    "DateBreak" timestamp with time zone,
    "Login" text,
    "Password" text,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_Applications" PRIMARY KEY ("Uid")
);

CREATE TABLE "RefreshTokens" (
    "Uid" uuid NOT NULL,
    "Value" text NOT NULL,
    "DateCreate" timestamp with time zone NOT NULL,
    "DateUpdate" timestamp with time zone NOT NULL,
    "DateExpire" timestamp with time zone NOT NULL,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_RefreshTokens" PRIMARY KEY ("Uid")
);

CREATE TABLE "WhiteIps" (
    "Uid" uuid NOT NULL,
    "Ip" text NOT NULL,
    "ApiKeyUid" uuid NOT NULL,
    "IsActive" boolean NOT NULL,
    CONSTRAINT "PK_WhiteIps" PRIMARY KEY ("Uid"),
    CONSTRAINT "FK_WhiteIps_ApiKeys_ApiKeyUid" FOREIGN KEY ("ApiKeyUid") REFERENCES "ApiKeys" ("Uid") ON DELETE CASCADE
);

CREATE TABLE "Roles" (
    "Uid" uuid NOT NULL,
    "Name" text NOT NULL,
    "Description" text,
    "ApplicationUid" uuid NOT NULL,
    CONSTRAINT "PK_Roles" PRIMARY KEY ("Uid"),
    CONSTRAINT "FK_Roles_Applications_ApplicationUid" FOREIGN KEY ("ApplicationUid") REFERENCES "Applications" ("Uid") ON DELETE CASCADE
);

CREATE TABLE "Users" (
    "Uid" uuid NOT NULL,
    "FirstName" text,
    "LastName" text,
    "MiddleName" text,
    "Login" text NOT NULL,
    "Language" integer NOT NULL,
    "Ip" text NOT NULL,
    "HashPassword" text,
    "Salt" bytea,
    "IsUserPass" boolean NOT NULL,
    "IsApiKey" boolean NOT NULL,
    "IsPass" boolean NOT NULL,
    "Email" text,
    "Code" text NOT NULL,
    "IsActiveCode" boolean NOT NULL,
    "DateCreate" timestamp with time zone NOT NULL,
    "DateUpdate" timestamp with time zone,
    "ApiKeyUid" uuid,
    "IsActive" boolean NOT NULL,
    "RefreshTokenUid" uuid,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Uid"),
    CONSTRAINT "FK_Users_ApiKeys_ApiKeyUid" FOREIGN KEY ("ApiKeyUid") REFERENCES "ApiKeys" ("Uid"),
    CONSTRAINT "FK_Users_RefreshTokens_RefreshTokenUid" FOREIGN KEY ("RefreshTokenUid") REFERENCES "RefreshTokens" ("Uid")
);

CREATE TABLE "UserRoles" (
    "Uid" uuid NOT NULL,
    "UserUid" uuid NOT NULL,
    "RoleUid" uuid NOT NULL,
    CONSTRAINT "PK_UserRoles" PRIMARY KEY ("Uid"),
    CONSTRAINT "FK_UserRoles_Roles_RoleUid" FOREIGN KEY ("RoleUid") REFERENCES "Roles" ("Uid") ON DELETE CASCADE,
    CONSTRAINT "FK_UserRoles_Users_UserUid" FOREIGN KEY ("UserUid") REFERENCES "Users" ("Uid") ON DELETE CASCADE
);

INSERT INTO "Applications" ("Uid", "DateBreak", "DateLastCheck", "Description", "Ip", "IsActive", "IsWork", "Login", "Name", "Password", "Port", "TypeApplication", "Version")
VALUES ('3f597bd4-b9dd-4788-aa02-8db6125ef945', NULL, NULL, 'База данных для пользователей', '195.133.28.197', TRUE, TRUE, 'postgres', 'UserBeer', '3G4rc3093jBlYgEaha/fOw==', 5432, 3, 'v1');
INSERT INTO "Applications" ("Uid", "DateBreak", "DateLastCheck", "Description", "Ip", "IsActive", "IsWork", "Login", "Name", "Password", "Port", "TypeApplication", "Version")
VALUES ('4e0129a9-fd5e-4110-95ff-cd19b43d3c72', NULL, NULL, 'База данных для волга-трекер', '195.133.28.197', TRUE, TRUE, 'postgres', 'volga_tracker', '3G4rc3093jBlYgEaha/fOw==', 5432, 3, 'v1');
INSERT INTO "Applications" ("Uid", "DateBreak", "DateLastCheck", "Description", "Ip", "IsActive", "IsWork", "Login", "Name", "Password", "Port", "TypeApplication", "Version")
VALUES ('79e32f14-c263-4a2e-9f1c-0185e8c03ce6', NULL, NULL, 'Приложения для списка задач, которые нужно сделать для восстановления Волги', '195.133.28.197', TRUE, TRUE, NULL, 'Volga-Tracker', NULL, 10, 2, 'v1');

INSERT INTO "Users" ("Uid", "ApiKeyUid", "Code", "DateCreate", "DateUpdate", "Email", "FirstName", "HashPassword", "Ip", "IsActive", "IsActiveCode", "IsApiKey", "IsPass", "IsUserPass", "Language", "LastName", "Login", "MiddleName", "RefreshTokenUid", "Salt")
VALUES ('09814dd3-e028-40ae-81ee-6b16653d57fa', NULL, '000000', TIMESTAMPTZ '2025-06-15T11:17:46.115311Z', NULL, NULL, 'Степан', NULL, '127.0.0.1', TRUE, FALSE, FALSE, FALSE, TRUE, 0, 'Кондрашов', 'Stepan', 'Андреевич', NULL, NULL);
INSERT INTO "Users" ("Uid", "ApiKeyUid", "Code", "DateCreate", "DateUpdate", "Email", "FirstName", "HashPassword", "Ip", "IsActive", "IsActiveCode", "IsApiKey", "IsPass", "IsUserPass", "Language", "LastName", "Login", "MiddleName", "RefreshTokenUid", "Salt")
VALUES ('4a087b1a-0195-493e-8324-4c26ebc0c4eb', NULL, '000000', TIMESTAMPTZ '2025-06-15T11:17:46.115115Z', NULL, NULL, 'Дмитрий', NULL, '127.0.0.1', TRUE, FALSE, FALSE, FALSE, TRUE, 0, 'Патюков', 'Totohka', 'Анатольевич', NULL, NULL);
INSERT INTO "Users" ("Uid", "ApiKeyUid", "Code", "DateCreate", "DateUpdate", "Email", "FirstName", "HashPassword", "Ip", "IsActive", "IsActiveCode", "IsApiKey", "IsPass", "IsUserPass", "Language", "LastName", "Login", "MiddleName", "RefreshTokenUid", "Salt")
VALUES ('5436fc04-a7d3-4a45-ad7f-861c1eba0ed0', NULL, '000000', TIMESTAMPTZ '2025-06-15T11:17:46.11531Z', NULL, NULL, 'Эдуард', NULL, '127.0.0.1', TRUE, FALSE, FALSE, FALSE, TRUE, 0, 'Новиков', 'Nedoff', 'Дмитриевич', NULL, NULL);
INSERT INTO "Users" ("Uid", "ApiKeyUid", "Code", "DateCreate", "DateUpdate", "Email", "FirstName", "HashPassword", "Ip", "IsActive", "IsActiveCode", "IsApiKey", "IsPass", "IsUserPass", "Language", "LastName", "Login", "MiddleName", "RefreshTokenUid", "Salt")
VALUES ('ad63676a-0994-427d-a639-17cb7ccf1fc2', NULL, '000000', TIMESTAMPTZ '2025-06-15T11:17:46.115312Z', NULL, NULL, 'Кирилл', NULL, '127.0.0.1', TRUE, FALSE, FALSE, FALSE, TRUE, 0, 'Шилов', 'Kirill', 'Александрович', NULL, NULL);

CREATE INDEX "IX_Roles_ApplicationUid" ON "Roles" ("ApplicationUid");

CREATE INDEX "IX_UserRoles_RoleUid" ON "UserRoles" ("RoleUid");

CREATE INDEX "IX_UserRoles_UserUid" ON "UserRoles" ("UserUid");

CREATE INDEX "IX_Users_ApiKeyUid" ON "Users" ("ApiKeyUid");

CREATE UNIQUE INDEX "IX_Users_RefreshTokenUid" ON "Users" ("RefreshTokenUid");

CREATE INDEX "IX_WhiteIps_ApiKeyUid" ON "WhiteIps" ("ApiKeyUid");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250615111747_Init', '9.0.2');

COMMIT;

