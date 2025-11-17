import type { CustomerModel } from "@/models/CustomerModel";
import type { EmployeeModel } from "@/models/EmployeeModel";
import type { LocationModel } from "@/models/LocationModel";
import type { SupplierModel } from "@/models/SupplierModel";

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
