import type { CustomerModel } from "./customerModel";
import type { EmployeeModel } from "./employeeModel";
import type { LocationModel } from "./locationModel";
import type { SupplierModel } from "./supplierModel";

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