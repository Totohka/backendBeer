import type { BaseModel } from './BaseModel';

export type RefreshToken = BaseModel & {
    Uid : string
    Value : string
    DateCreate : Date
    DateUpdate : Date
    DateExpire : Date
    IsActive : boolean
}