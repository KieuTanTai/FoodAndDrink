import type { AccountModel } from "./account-model";
import type { RoleModel } from "./role-model";

export interface RolesOfUserKey {
    accountId: number;
    roleId: number;
}

export interface RolesOfUserModel {
    id: number;
    accountId: number;
    roleId: number;
    addedDate: string;
    account: AccountModel | null;
    role: RoleModel | null;
}