import type { BaseModel } from './BaseModel';

export type UserRole = BaseModel & {
    Uid : string
    UserUid : string
    RoleUid : string
}