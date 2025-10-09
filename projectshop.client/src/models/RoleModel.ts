import type { RolesOfUserModel } from "./roles-of-user-model";

export interface RoleModel {
    roleId: number;
    roleName: string;
    roleStatus: boolean;
    roleCreatedDate: string;
    roleLastUpdatedDate: string;
    rolesOfUsers: RolesOfUserModel[] | [];
}