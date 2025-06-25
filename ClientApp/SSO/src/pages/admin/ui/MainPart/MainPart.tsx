import { useLocation, useSearchParams } from "react-router-dom";
import { TableAdmin } from "./TableAdmin/TableAdmin";
import type { ApiKey } from "../../model/ApiKey";
import type { Application } from "../../model/Application";
// FIXME: не используется
import type { BaseModel } from "../../model/BaseModel";
import { createListCollection } from "@chakra-ui/react";

// FIXME: лучше все хелпер функции вынести в отдельный файл/папку, а вообще используя FSD есть сегмент lib для такого. Относиться ко всем нижнем функциям
// TODO: а не легче просто константы сделать? ну типа меньше кода, скобок всей хуйни этой. Ко всем нижним функциям (также стоит вынести в отдельный файл/папку или сегмент (скорее всего также сегмент lib))
function getKeys() {
  const keys: ApiKey[] = [
    {
      Uid: "2345678",
      Name: "string",
      Value: "string",
      DateCreate: new Date(),
      DateExpire: new Date(),
      IsActive: true,
    },
    {
      Uid: "2345678",
      Name: "string",
      Value: "string",
      DateCreate: new Date(),
      DateExpire: new Date(),
      IsActive: true,
    },
  ];
  return keys;
}

function getApplications() {
  const app: Application[] = [
    {
      Uid: "12324567",
      Name: "string",
      Description: "string",
      Version: "string",
      Ip: "string",
      Port: 24,
      IsWork: true,
      DateLastCheck: new Date(),
      DateBreak: new Date(),
      IsActive: true,
      Login: null,
      Password: null,
      TypeApplication: 1,
    },
    {
      Uid: "12312324567",
      Name: "string",
      Description: "string",
      Version: "string",
      Ip: "string",
      Port: 27,
      IsWork: true,
      DateLastCheck: new Date(),
      DateBreak: new Date(),
      IsActive: true,
      Login: null,
      Password: null,
      TypeApplication: 1,
    },
    {
      Uid: "12324567",
      Name: "string",
      Description: "string",
      Version: "string",
      Ip: "string",
      Port: 24,
      IsWork: true,
      DateLastCheck: new Date(),
      DateBreak: new Date(),
      IsActive: true,
      Login: null,
      Password: null,
      TypeApplication: 1,
    },
    {
      Uid: "12312324567",
      Name: "string",
      Description: "string",
      Version: "string",
      Ip: "string",
      Port: 27,
      IsWork: true,
      DateLastCheck: new Date(),
      DateBreak: new Date(),
      IsActive: true,
      Login: null,
      Password: null,
      TypeApplication: 1,
    },
  ];
  return app;
}

function getHeadersApiKey() {
  return ["Uid", "Name", "Value", "DateCreate", "DateExpire", "IsActive"];
}
function getHeadersApplication() {
  return [
    "Uid",
    "Name",
    "Description",
    "Version",
    "Ip",
    "Port",
    "IsWork",
    "DateLastCheck",
    "DateBreak",
    "IsActive",
    "Login",
    "Password",
    "TypeApplication",
  ];
}
function getHeadersRefreshToken() {
  return ["Uid", "Value", "DateCreate", "DateUpdate", "DateExpire", "IsActive"];
}
function getHeadersRole() {
  return ["Uid", "Name", "Description", "ApplicationUid"];
}
function getHeadersUserRole() {
  return ["Uid", "UserUid", "RoleUid"];
}
function getHeadersUser() {
  return [
    "Uid",
    "FirstName",
    "LastName",
    "MiddleName",
    "Login",
    "Ip",
    "HashPassword",
    "Salt",
    "IsUserPass",
    "IsApiKey",
    "IsPass",
    "Email",
    "Code",
    "IsActiveCode",
    "DateCreate",
    "DateUpdate",
    "ApiKeyUid",
    "IsActive",
    "RefreshTokenUid",
    "Language",
  ];
}
function getHeadersWhiteList() {
  return ["Uid", "Ip", "ApiKeyUid", "IsActive"];
}

// FIXME: не очень понятно что это из названия компонента
export const MainPart = () => {
  /* FIXME: Как будто можно не создавать такую сложную штуку ну типа:
  const location = useLocation();
  const model = new URLSearchParams(location).search.get("model")
  а то типа кастомный хук, плюс его использование, плюс еще взять в свитче

  еще можно юзнуть просто useSearchParams, он есть в 6 версии react-router
  const [searchParams, setSearchParams] = useSearchParams();
  searchParams.get("model")
  */
  // FIXME: так или иначе при создании кастомного хука его надо выносить за пределы компонента, чтобы он каждый раз не создавался и не тригерил создание всех остальных хуков, что может повлечь лишние ререндеры и другие проблемы в реакте
  const useQuery = () => new URLSearchParams(useLocation().search);
  const query = useQuery();
  // TODO: мб нет, но как будто можно убрать функцию и тупа отдавать через switch
  const render = () => {
    switch (query.get("model")) {
      // TODO: а так делают? ну то что query параметр регистрозависимый
      case "ApiKeys":
        return <TableAdmin headers={getHeadersApiKey()} models={getKeys()} />;
      case "Applications":
        return (
          <TableAdmin
            headers={getHeadersApplication()}
            models={getApplications()}
          />
        );
      case "RefreshTokens":
        return <TableAdmin headers={getHeadersRefreshToken()} models={[]} />;
      case "Roles":
        return <TableAdmin headers={getHeadersRole()} models={[]} />;
      case "UserRoles":
        return <TableAdmin headers={getHeadersUserRole()} models={[]} />;
      case "Users":
        return <TableAdmin headers={getHeadersUser()} models={[]} />;
      case "WhiteList":
        return <TableAdmin headers={getHeadersWhiteList()} models={[]} />;
      default:
        return <></>;
    }
  };

  // FIXME: лишние фрагменты
  return <>{render()}</>;
};
