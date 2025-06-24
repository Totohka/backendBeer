import type { BaseModel } from './BaseModel';

export type ApiKey = BaseModel & {
    Uid : string
    Name : string
    Value : string
    DateCreate : Date
    DateExpire : Date
    IsActive : boolean
}