import type { SupplierModel } from './supplierModel';
import type { LocationModel } from "./locationModel";
import type { CustomerModel } from './customerModel';
import type { EmployeeModel } from './employeeModel';

export interface LocationDistrictModel {
    locationDistrictId: number;
    locationDistrictName: string;
    locationDistrictStatus: boolean;
    locations: LocationModel[] | [];
    companySuppliers: SupplierModel[] | [];
    storeSuppliers: SupplierModel[] | [];
    customers: CustomerModel[] | [];
    employees: EmployeeModel[] | [];
}