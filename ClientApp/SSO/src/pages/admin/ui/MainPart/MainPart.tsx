import { useLocation } from "react-router-dom"
import { TableAdmin } from "./TableAdmin/TableAdmin";
import { createListCollection } from "@chakra-ui/react";
import type { ApiKey } from '../../model/ApiKey';
import type { Application } from "../../model/Application";
import type { BaseModel } from "../../model/BaseModel";

function getKeys() {
    const keys: ApiKey[] = 
        [
            {
                Uid : "2345678",
                Name : "string",
                Value : "string",
                DateCreate : new Date(),
                DateExpire : new Date(),
                IsActive : true
            }, 
            {
                Uid : "2345678",
                Name : "string",
                Value : "string",
                DateCreate : new Date(),
                DateExpire : new Date(),
                IsActive : true
            }
        ];
    return keys;
}

function getApplications() {
    const app: Application[] = 
        [
            {
                Uid : "12324567",
                Name : "string",
                Description : "string",
                Version : "string",
                Ip : "string",
                Port : 24,
                IsWork : true,
                DateLastCheck : new Date(),
                DateBreak : new Date(),
                IsActive : true,
                Login : null,
                Password : null,
                TypeApplication : 1
            }, 
            {
                Uid : "12312324567",
                Name : "string",
                Description : "string",
                Version : "string",
                Ip : "string",
                Port : 27,
                IsWork : true,
                DateLastCheck : new Date(),
                DateBreak : new Date(),
                IsActive : true,
                Login : null,
                Password : null,
                TypeApplication : 1
            },
            {
                Uid : "12324567",
                Name : "string",
                Description : "string",
                Version : "string",
                Ip : "string",
                Port : 24,
                IsWork : true,
                DateLastCheck : new Date(),
                DateBreak : new Date(),
                IsActive : true,
                Login : null,
                Password : null,
                TypeApplication : 1
            }, 
            {
                Uid : "12312324567",
                Name : "string",
                Description : "string",
                Version : "string",
                Ip : "string",
                Port : 27,
                IsWork : true,
                DateLastCheck : new Date(),
                DateBreak : new Date(),
                IsActive : true,
                Login : null,
                Password : null,
                TypeApplication : 1
            }
        ];
    return app;
}

function getHeadersApiKey(){
    return ["Uid", "Name", "Value", "DateCreate", "DateExpire", "IsActive"]
}
function getHeadersApplication(){
    return ["Uid", "Name", "Description", "Version", "Ip", "Port", "IsWork", "DateLastCheck", "DateBreak", "IsActive", "Login", "Password", "TypeApplication"]
}
function getHeadersRefreshToken(){
    return ["Uid", "Value", "DateCreate", "DateUpdate", "DateExpire", "IsActive"]
}
function getHeadersRole(){
    return ["Uid", "Name", "Description", "ApplicationUid"]
}
function getHeadersUserRole(){
    return ["Uid", "UserUid", "RoleUid"]
}
function getHeadersUser(){
    return ["Uid", "FirstName", "LastName", "MiddleName", "Login", "Ip", "HashPassword", "Salt", "IsUserPass", "IsApiKey", 
        "IsPass", "Email", "Code", "IsActiveCode", "DateCreate", "DateUpdate", "ApiKeyUid", "IsActive", "RefreshTokenUid", "Language"]
}
function getHeadersWhiteList(){
    return ["Uid", "Ip", "ApiKeyUid", "IsActive"]
}

export const MainPart = () => {
    const useQuery = () => new URLSearchParams(useLocation().search);
    const query = useQuery();
    const render = () => {
        switch (query.get("model")){
            case "ApiKeys": 
                return <TableAdmin headers={getHeadersApiKey()} models={getKeys()} />
            case "Applications": 
                return <TableAdmin headers={getHeadersApplication()} models={getApplications()} />
            case "RefreshTokens": 
                return <TableAdmin headers={getHeadersRefreshToken()} models={[]} />
            case "Roles": 
                return <TableAdmin headers={getHeadersRole()} models={[]} />
            case "UserRoles": 
                return <TableAdmin headers={getHeadersUserRole()} models={[]} />
            case "Users": 
                return <TableAdmin headers={getHeadersUser()}models={[]} />
            case "WhiteList": 
                return <TableAdmin headers={getHeadersWhiteList()} models={[]} />
            default:
                return <></>
        }
    }
    
    return (
        <>
            {render()}
        </>
    )
}