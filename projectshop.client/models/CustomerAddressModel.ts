import type { LocationWardModel } from "@/models/LocationWardModel";
import type { LocationDistrictModel } from "@/models/LocationDistrictModel";
import type { LocationCityModel } from "@/models/LocationCityModel";
import type { CustomerModel } from "@/models/CustomerModel";
export interface CustomerAddressModel {
  customerAddressId: number;
  customerCityId: number;
  customerDistrictId: number;
  customerWardId: number;
  customerId: number;
  customerStreet: string;
  customerAddressNumber: string;
  customerAddressStatus: boolean;
  city: LocationCityModel | null;
  district: LocationDistrictModel | null;
  ward: LocationWardModel | null;
  customer: CustomerModel | null;
}
