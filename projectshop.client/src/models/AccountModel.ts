import type{ RolesOfUserModel } from './roles-of-user-model';
import type{ CustomerModel } from "./customer-model";
import type{ EmployeeModel } from "./employee-model";

export interface AccountModel {
    accountId: number;
    userName: string;
    password: string;
    accountCreatedDate: string;
    accountLastUpdatedDate: string;
    accountStatus: boolean;
    customer: CustomerModel | null;
    employee: EmployeeModel | null;
    rolesOfUsers: RolesOfUserModel[] | [];
}