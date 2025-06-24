import type { BaseModel } from './BaseModel';

export type WhiteList = BaseModel & {
    Uid : string
    Ip : string
    ApiKeyUid : string
    IsActive : boolean
}