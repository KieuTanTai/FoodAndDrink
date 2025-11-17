import type { AccountModel } from "@/models/AccountModel";
import type { RoleModel } from "@/models/RoleModel";

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
