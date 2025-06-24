import type { BaseModel } from './BaseModel';

export type Role = BaseModel & {
    Uid : string
    Name : string
    Description : string
    ApplicationUid : string
}