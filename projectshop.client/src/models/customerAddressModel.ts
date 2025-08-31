import { LocationWardModel } from './locationWardModel';
import { LocationDistrictModel } from './locationDistrictModel';
import { LocationCityModel } from './locationCityModel';
import { CustomerModel } from './customerModel';
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