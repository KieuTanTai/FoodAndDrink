import type{ LocationWardModel } from './locationWardModel';
import type{ LocationDistrictModel } from './locationDistrictModel';
import type{ LocationCityModel } from './locationCityModel';
import type{ CustomerModel } from './customerModel';
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