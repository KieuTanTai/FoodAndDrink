import { RolesOfUserModel } from "./rolesOfUserModel";

export interface RoleModel {
    roleId: number;
    roleName: string;
    roleStatus: boolean;
    roleCreatedDate: string;
    roleLastUpdatedDate: string;
    rolesOfUsers: RolesOfUserModel[] | [];
}