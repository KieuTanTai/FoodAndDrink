import type{ SupplierModel } from './supplier-model';
import type{ LocationModel } from "./location-model";
import type{ CustomerModel } from './customer-model';
import type{ EmployeeModel } from './employee-model';

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