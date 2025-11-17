import type { RolesOfUserModel } from "@/models/RolesOfUserModel";
import type { CustomerModel } from "@/models/CustomerModel";
import type { EmployeeModel } from "@/models/EmployeeModel";

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
