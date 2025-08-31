import { CustomerModel } from "./customerModel";
import { EmployeeModel } from "./employeeModel";
import { LocationModel } from "./locationModel";
import { SupplierModel } from "./supplierModel";

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