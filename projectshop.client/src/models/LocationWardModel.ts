import type { CustomerModel } from "./customer-model";
import type { EmployeeModel } from "./employee-model";
import type { LocationModel } from "./location-model";
import type { SupplierModel } from "./supplier-model";

export interface LocationWardModel {
    locationWardId: number;
    locationWardName: string;
    locationWardStatus: boolean;
    locations: LocationModel[] | [];
    companySuppliers: SupplierModel[] | [];
    storeSuppliers: SupplierModel[] | [];
    customers: CustomerModel[] | [];
    employees: EmployeeModel[] | [];
}