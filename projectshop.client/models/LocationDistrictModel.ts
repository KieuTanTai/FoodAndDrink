import type { SupplierModel } from "@/models/SupplierModel";
import type { LocationModel } from "@/models/LocationModel";
import type { CustomerModel } from "@/models/CustomerModel";
import type { EmployeeModel } from "@/models/EmployeeModel";

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
