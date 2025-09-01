import type { AccountModel } from "./accountModel";
import type { RoleModel } from "./roleModel";

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