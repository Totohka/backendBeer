import type { BaseModel } from './BaseModel';

export type Application = BaseModel & {
    Uid : string
    Name : string
    Description : string
    Version : string
    Ip : string
    Port : number
    IsWork : boolean
    DateLastCheck : Date
    DateBreak : Date
    IsActive : boolean
    Login? : string | null
    Password? : string | null
    TypeApplication : number
}