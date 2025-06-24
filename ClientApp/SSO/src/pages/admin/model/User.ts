import type { BaseModel } from './BaseModel';

export type User = BaseModel & {
    Uid : string
    FirstName : string
    LastName : string
    MiddleName : string
    Login : string
    Ip : string
    HashPassword : string
    Salt : string
    IsUserPass : string
    IsApiKey : string
    IsPass : string
    Email : string
    Code : string
    IsActiveCode : boolean
    DateCreate : Date
    DateUpdate : Date
    ApiKeyUid : string
    IsActive : boolean
    RefreshTokenUid : string
    Language : string
}