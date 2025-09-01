import type{ SupplierModel } from './supplierModel';
import type{ LocationModel } from "./locationModel";
import type{ CustomerModel } from './customerModel';
import type{ EmployeeModel } from './employeeModel';

export interface LocationCityModel {
    locationCityId: number;
    locationCityName: string;
    locationCityStatus: boolean;
    locations: LocationModel[] | [];
    companySuppliers: SupplierModel[] | [];
    storeSuppliers: SupplierModel[] | [];
    customers: CustomerModel[] | [];
    employees: EmployeeModel[] | [];
}