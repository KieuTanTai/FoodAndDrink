import { RolesOfUserModel } from './rolesOfUserModel';
import { CustomerModel } from "./customerModel";
import { EmployeeModel } from "./employeeModel";

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