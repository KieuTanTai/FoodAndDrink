import { SupplierModel } from './supplierModel';
import { LocationModel } from "./locationModel";
import { CustomerModel } from './customerModel';
import { EmployeeModel } from './employeeModel';

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